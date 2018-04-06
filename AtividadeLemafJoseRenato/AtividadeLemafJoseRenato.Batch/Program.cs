using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util.Log;
using AtividadeLemafJoseRenato.Util;
using Fronteira.Dtos;
using System;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Geral;
using System.Configuration;

namespace AtividadeLemafJoseRenato.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando aplicação...");
            Console.WriteLine("Inicializando banco de dados...");
            InicializadorBanco.InicializaBanco();
            Console.WriteLine("Lendo entradas");
            try
            {
                LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
                {
                    CaminhoArquivoEntrada = "Entrada.txt"
                };
                LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);

                Console.WriteLine($"Número de entradas é {resultadoEntrada.ListaInformacoesAgendamentoReuniao.Count}");

                var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();

                foreach (AgendamentoDto agendamentoDto in resultadoEntrada.ListaInformacoesAgendamentoReuniao)
                {
                    Console.WriteLine($"Avaliar entrada {agendamentoDto.EntradaBruta}:");
                    SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                    {
                        InformacoesAgendamentoSala = agendamentoDto
                    };
                    SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
                    if (resultadoSolicitarSala.IdSalaAgendada.HasValue)
                        Console.WriteLine($"Sala agendada: Sala {resultadoSolicitarSala.IdSalaAgendada.Value}");
                    else
                    {
                        Console.WriteLine("Não foi possível agendar uma sala. Sugestões:");
                        resultadoSolicitarSala.ListaSugestoesAgendamentos.ForEach(sugestao =>
                            Console.WriteLine($"Sala {sugestao.IdSalaSugerida}, DataInicio: {sugestao.DataInicioSugerida.ToString("dd-MM-yyyy HH:mm")}, DataFim {sugestao.DataFimSugerida.ToString("dd-MM-yyyy HH:mm")} "));
                    }
                }
                Console.WriteLine("Aperte qualquer tecla para finalizar!");
                Console.ReadKey();
            }
            catch(InformacaoException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Aperte qualquer tecla para finalizar!");
                Console.ReadKey();
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
                Console.WriteLine("Ocorreu um erro, favor contator o administrador do sistema!");
                    Console.WriteLine("Aperte qualquer tecla para finalizar!");
                Console.ReadKey();
            }
        }
    }
}
