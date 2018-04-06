using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util;
using AtividadeLemafJoseRenato.Util.Log;
using Fronteira.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Tests.Reuniao
{
    [TestClass]
    public class SelecionarOuSugerirSalaTeste : TesteBase
    {
        public SelecionarOuSugerirSalaTeste() : base() { }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void DataInicialMaiorQueFinalTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = DateTime.Now,
                    DataFim = DateTime.Now.AddHours(-2)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void ReuniaoDuracaoMaiorPermitidoTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = DateTime.Now.AddDays(1),
                    DataFim = DateTime.Now.AddDays(1).AddHours(ParametrosRegraAgendamento.MaximoHorasDuracaoReuniao).AddMilliseconds(1)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void ReuniaoAgendadaNoPassadoTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = DateTime.Now.AddDays(1).AddHours(-2),
                    DataFim = DateTime.Now.AddDays(1)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void ReuniaoAgendadaMenosPermitidoTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = DateTime.Now.AddHours(1),
                    DataFim = DateTime.Now.AddHours(3)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void ReuniaoAgendadaMaisMaximoPermitidoTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = DateTime.Now.AddDays(ParametrosRegraAgendamento.MaximoDiaAntecedenciaAgendamento + 1),
                    DataFim = DateTime.Now.AddDays(ParametrosRegraAgendamento.MaximoDiaAntecedenciaAgendamento + 1).AddHours(3)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void ReuniaoAgendadaDiaNaoUtilTeste()
        {
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            DateTime diaNaoUtil = DateTime.Now;
            while (diaNaoUtil.DayOfWeek != DayOfWeek.Saturday && diaNaoUtil.DayOfWeek != DayOfWeek.Sunday)
            {
                diaNaoUtil = diaNaoUtil.AddDays(1);
            }
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = diaNaoUtil,
                    DataFim = diaNaoUtil.AddHours(3)
                });
            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();
            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void ReuniaoAgendamentoValido()
        {
            int[] salasEsperadas = { 1, 2, 6 };
            int contador = 0;
            bool deuCerto = true;
            List<AgendamentoDto> ListaInformacoesAgendamentoReuniao = new List<AgendamentoDto>();
            DateTime proximoDiaUtil = ObterProximoDiaUtil();
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = proximoDiaUtil,
                    DataFim = proximoDiaUtil.AddHours(3),
                    NecessitaAcessoInternet = true,
                    NecessitaTvEWebcam = true,
                    QuantidadePessoas = 10
                });
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = proximoDiaUtil,
                    DataFim = proximoDiaUtil.AddHours(3),
                    NecessitaAcessoInternet = true,
                    NecessitaTvEWebcam = true,
                    QuantidadePessoas = 10
                });
            ListaInformacoesAgendamentoReuniao.Add(
                new AgendamentoDto()
                {
                    DataInicio = proximoDiaUtil,
                    DataFim = proximoDiaUtil.AddHours(3),
                    NecessitaAcessoInternet = true,
                    NecessitaTvEWebcam = false,
                    QuantidadePessoas = 10
                });

            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();

            foreach (AgendamentoDto agendamentoDto in ListaInformacoesAgendamentoReuniao)
            {
                SelecionarOuSugerirSalaRequisicao requisicaoSolicitarSala = new SelecionarOuSugerirSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarOuSugerirSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);

                deuCerto = deuCerto && resultadoSolicitarSala.IdSalaAgendada.HasValue 
                    && resultadoSolicitarSala.IdSalaAgendada.Value == salasEsperadas[contador];
                contador++;
            }
            Assert.IsTrue(deuCerto);
        }

        private DateTime ObterProximoDiaUtil()
        {
            DateTime diaUtil = DateTime.Now.AddDays(1);
            while (diaUtil.DayOfWeek == DayOfWeek.Saturday || diaUtil.DayOfWeek == DayOfWeek.Sunday)
            {
                diaUtil = diaUtil.AddDays(1);
            }

            return diaUtil;
        }
    }
}
