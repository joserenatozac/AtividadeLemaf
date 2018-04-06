using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Geral;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util;
using AtividadeLemafJoseRenato.Util.Log;
using Fronteira.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtividadeLemafJoseRenato.Controllers
{
    public class ReuniaoController : Controller
    {
        // GET: Reuniao
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AgendarReuniao(DateTime dataInicio, DateTime dataFim, int quantidadePessoas,
            bool precisaInternet, bool precisaWebcam)
        {
            try
            {
                var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();

                AgendamentoDto agendamentoDto = new AgendamentoDto()
                {
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    QuantidadePessoas = quantidadePessoas,
                    NecessitaAcessoInternet = precisaInternet,
                    NecessitaTvEWebcam = precisaWebcam
                };
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
                if (resultadoSolicitarSala.IdSalaAgendada.HasValue)
                    return Json(new { sucesso = true, sala = resultadoSolicitarSala.IdSalaAgendada.Value }, JsonRequestBehavior.AllowGet);
                else
                {
                    string mensagem = "Não foi possível agendar uma sala. Sugestões:"; 
                    string sugestoes = string.Empty;
                    foreach(var sugestaoDto in resultadoSolicitarSala.ListaSugestoesAgendamentos)
                    {
                        sugestoes = sugestoes + $" Sala {sugestaoDto.IdSalaSugerida}, DataInicio: {sugestaoDto.DataInicioSugerida.ToString("dd-MM-yyyy HH:mm")}, DataFim {sugestaoDto.DataFimSugerida.ToString("dd-MM-yyyy HH:mm")} ";
                    }
                    return Json(new { sucesso = false, mensagem = mensagem }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (InformacaoException ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                EnviarEmailRequisicao requisicaoEnviarEmail = new EnviarEmailRequisicao(new LogContexto("EnviarEmail", null))
                {

                    Assunto = "Erro na aplicação Agendamento de Reunião",
                    CorpoEmail = ex.Message + ex.InnerException + ex.StackTrace,
                    Remetente = ConfigurationManager.AppSettings["EMAIL_REMETENTE"],
                    Destinatario = ConfigurationManager.AppSettings["EMAIL_TESTE"],
                };
                new EnviarEmailExecutor().Executar(requisicaoEnviarEmail);
            }
            return Json(new { sucesso = false, mensagem = "Ocorreu um erro!" }, JsonRequestBehavior.AllowGet);
        }
    }
}