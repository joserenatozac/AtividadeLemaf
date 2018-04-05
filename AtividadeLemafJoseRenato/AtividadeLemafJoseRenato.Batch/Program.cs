using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util.Log;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace AtividadeLemafJoseRenato.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {
            
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
