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
    public class LerEntradaTeste : TesteBase
    {
        public LerEntradaTeste() : base() { }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void HoraErradaTeste()
        {
            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
            {
                CaminhoArquivoEntrada = "..\\..\\HoraErradaTeste.txt"
            };
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void DataErradaTeste()
        {
            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
            {
                CaminhoArquivoEntrada = "..\\..\\DataErradaTeste.txt"
            };
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InformacaoException))]
        public void EntradaVaziaTeste()
        {
            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
            {
                CaminhoArquivoEntrada = "..\\..\\EntradaVaziaTeste.txt"
            };
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void EntradaValidaTeste()
        {
            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
            {
                CaminhoArquivoEntrada = "..\\..\\EntradaValidaTeste.txt"
            };
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);
            AgendamentoDto agendamentoValido = resultadoEntrada.ListaInformacoesAgendamentoReuniao.FirstOrDefault();

            Assert.IsTrue(agendamentoValido.DataInicio == new DateTime(2018, 04,26, 10,0,0) &&
                agendamentoValido.DataFim == new DateTime(2018, 04, 26, 12, 0, 0) &&
                agendamentoValido.NecessitaAcessoInternet == true && agendamentoValido.NecessitaTvEWebcam == true
                );
        }
    }
}
