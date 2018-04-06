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

            var selecionarExecutor = new SelecionarOuSugerirSalaExecutor();

            foreach(AgendamentoDto agendamentoDto in resultadoEntrada.ListaInformacoesAgendamentoReuniao)
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
    }
}
