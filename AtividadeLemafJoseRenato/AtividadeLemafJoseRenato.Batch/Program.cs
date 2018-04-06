using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util.Log;
using AtividadeLemafJoseRenato.Util;

namespace AtividadeLemafJoseRenato.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {
            InicializadorBanco.InicializaBanco();
            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null));
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);

            SelecionarSalaRequisicao requisicaoSolicitarSala = new SelecionarSalaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
            {
                InformacoesAgendamentoSala = resultadoEntrada.InformacoesAgendamentoReuniao
            };
            SelecionarSalaResultado resultadoSolicitarSala = new SelecionarSalaExecutor().Executar(requisicaoSolicitarSala);
        }
    }
}
