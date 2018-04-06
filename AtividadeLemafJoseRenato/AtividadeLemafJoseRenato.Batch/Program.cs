using AtividadeLemafJoseRenato.Executores.Reuniao;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util.Log;
using AtividadeLemafJoseRenato.Util;
using Fronteira.Dtos;
using System;

namespace AtividadeLemafJoseRenato.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Inicializando aplicação...");
            Console.Write("Inicializando banco de dados...");
            InicializadorBanco.InicializaBanco();
            Console.Write("Lendo entradas");

            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null));
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);

            Console.Write($"Número de entradas é {resultadoEntrada.ListaInformacoesAgendamentoReuniao.Count}");

            var selecionarExecutor = new SelecionarSalaExecutor();

            foreach(AgendamentoDto agendamentoDto in resultadoEntrada.ListaInformacoesAgendamentoReuniao)
            {
                Console.Write($"Avaliar entrada {agendamentoDto.EntradaBruta}:");
                SelecionarSalaRequisicao requisicaoSolicitarSala = new SelecionarSalaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
                Console.Write($"Sala agendada: Sala {resultadoSolicitarSala.IdSalaAgendada}");
            }
        }
    }
}
