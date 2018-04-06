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
            Console.WriteLine("Inicializando aplicação...");
            Console.WriteLine("Inicializando banco de dados...");
            InicializadorBanco.InicializaBanco();
            Console.WriteLine("Lendo entradas");

            LerEntradaRequisicao requisicaoEntrada = new LerEntradaRequisicao(new LogContexto(TipoFluxoLog.LerEntrada, null));
            LerEntradaResultado resultadoEntrada = new LerEntradaExecutor().Executar(requisicaoEntrada);

            Console.WriteLine($"Número de entradas é {resultadoEntrada.ListaInformacoesAgendamentoReuniao.Count}");

            var selecionarExecutor = new SelecionarSalaExecutor();

            foreach(AgendamentoDto agendamentoDto in resultadoEntrada.ListaInformacoesAgendamentoReuniao)
            {
                Console.WriteLine($"Avaliar entrada {agendamentoDto.EntradaBruta}:");
                SelecionarSalaRequisicao requisicaoSolicitarSala = new SelecionarSalaRequisicao(new LogContexto(TipoFluxoLog.SelecionarSala, null))
                {
                    InformacoesAgendamentoSala = agendamentoDto
                };
                SelecionarSalaResultado resultadoSolicitarSala = selecionarExecutor.Executar(requisicaoSolicitarSala);
                Console.WriteLine($"Sala agendada: Sala {resultadoSolicitarSala.IdSalaAgendada}");
            }
        }
    }
}
