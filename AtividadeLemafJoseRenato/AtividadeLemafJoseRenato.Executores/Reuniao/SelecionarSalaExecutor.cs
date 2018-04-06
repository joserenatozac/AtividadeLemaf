using AtividadeLemafJoseRenato.Fronteiras.Executor;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fronteira.Dtos;
using AtividadeLemafJoseRenato.Util.Log;
using AtividadeLemafJoseRenato.Util;
using AtividadeLemafJoseRenato.Entidades;
using AtividadeLemafJoseRenato.Repositorios;
using AtividadeLemafJoseRenato.Fronteiras.Repositorios;

namespace AtividadeLemafJoseRenato.Executores.Reuniao
{
    public class SelecionarSalaExecutor : ExecutorBase, IExecutor<SelecionarSalaRequisicao, SelecionarSalaResultado>
    {
        private readonly ISalaRepositorio salaRepositorio;
        private readonly IHistoricoSalaRepositorio historicoSalaRepositorio;

        public SelecionarSalaExecutor()
        {
            salaRepositorio = new SalaRepositorio();
            historicoSalaRepositorio = new HistoricoSalaRepositorio();
        }

        public SelecionarSalaResultado Executar(SelecionarSalaRequisicao requisicao)
        {
            SelecionarSalaResultado resultado = new SelecionarSalaResultado();
            List<SalaEntidade> salasIdeias;
            List<SalaEntidade> salasSubutilizadas;
            InformacoesLog = requisicao.InformacoesLog;
            ValidarInformacoesAgendamento(requisicao.InformacoesAgendamentoSala);
            List<SalaEntidade> listaSalas = salaRepositorio.ListarTodas();
            List<SalaEntidade> salasDisponiveisNoHorario = ListarSalasDisponiveisNoHorario(requisicao, listaSalas);
            salasIdeias = BuscarSalasIdeais(requisicao.InformacoesAgendamentoSala, salasDisponiveisNoHorario);
            if (!salasIdeias.Any())
            {
                salasSubutilizadas = BuscarSubutilizadas(requisicao.InformacoesAgendamentoSala, salasDisponiveisNoHorario);
                resultado.IdSalaAgendada = salasSubutilizadas.OrderBy(sala => sala.QuantidadePessoas).ThenBy(sala => sala.IdSala).FirstOrDefault()?.IdSala;
            }
            else
            {
                resultado.IdSalaAgendada = salasIdeias.OrderBy(sala => sala.QuantidadePessoas).ThenBy(sala => sala.IdSala).FirstOrDefault()?.IdSala;
            }
            if (resultado.IdSalaAgendada.HasValue)
                RealizarAgendamento(resultado.IdSalaAgendada.Value, requisicao.InformacoesAgendamentoSala.DataInicio, requisicao.InformacoesAgendamentoSala.DataFim);
            return resultado;
        }

        private void RealizarAgendamento(int idSalaAgendada, DateTime dataInicio, DateTime dataFim)
        {
            historicoSalaRepositorio.Inserir(idSalaAgendada, dataInicio, dataFim);
        }

        private List<SalaEntidade> BuscarSubutilizadas(AgendamentoDto informacoesAgendamentoSala, List<SalaEntidade> salasDisponiveisNoHorario)
        {
            IEnumerable<SalaEntidade> salasSubutilizadas = salasDisponiveisNoHorario;
            if (informacoesAgendamentoSala.NecessitaAcessoInternet)
                salasSubutilizadas = salasSubutilizadas.Where(sala => sala.TemAcessoInternet == informacoesAgendamentoSala.NecessitaAcessoInternet);
            if (informacoesAgendamentoSala.NecessitaTvEWebcam)
                salasSubutilizadas = salasSubutilizadas.Where(sala => sala.TemWebcamConferencia == informacoesAgendamentoSala.NecessitaTvEWebcam);
            return salasSubutilizadas.Where(sala => sala.QuantidadePessoas >= informacoesAgendamentoSala.QuantidadePessoas).ToList();
        }

        private List<SalaEntidade> BuscarSalasIdeais(AgendamentoDto informacoesAgendamentoSala, List<SalaEntidade> salasDisponiveisNoHorario)
        {
            return salasDisponiveisNoHorario.Where(sala => sala.TemAcessoInternet == informacoesAgendamentoSala.NecessitaAcessoInternet
                && sala.TemWebcamConferencia == informacoesAgendamentoSala.NecessitaTvEWebcam && sala.QuantidadePessoas >= informacoesAgendamentoSala.QuantidadePessoas).ToList();
        }

        private List<SalaEntidade> ListarSalasDisponiveisNoHorario(SelecionarSalaRequisicao requisicao, List<SalaEntidade> listaSalas)
        {
            List<HistoricoSalaEntidade> listaSalasOcupadas = historicoSalaRepositorio.ListarSalasOcupadas(
                requisicao.InformacoesAgendamentoSala.DataInicio, requisicao.InformacoesAgendamentoSala.DataFim);

            List<SalaEntidade> salasDisponiveisNoHorario = listaSalas;
            listaSalasOcupadas.ForEach
                (
                    salaOcupada => salasDisponiveisNoHorario.Remove
                    (
                        salasDisponiveisNoHorario.FirstOrDefault(sala => sala.IdSala == salaOcupada.IdSala)
                    )
                );
            return salasDisponiveisNoHorario;
        }

        private void ValidarInformacoesAgendamento(AgendamentoDto informacoesAgendamentoSala)
        {
            InformacoesLog.TipoLog = TipoLog.Informacao;
            if(informacoesAgendamentoSala.DataFim.Subtract(informacoesAgendamentoSala.DataInicio).TotalMilliseconds < 0)
            {
                throw new InformacaoException("A data final da reunião é menor que a data inicial.", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataFim.Subtract(informacoesAgendamentoSala.DataInicio).TotalHours 
                > ParametrosRegraAgendamento.MaximoHorasDuracaoReuniao)
            {
                throw new InformacaoException("A data final da reunião é menor que a data inicial.", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataInicio.Subtract(DateTime.Now).TotalDays <= 0)
            {
                throw new InformacaoException($"A reunião deve ser agendada para uma data maior que a atual.", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataInicio.Subtract(DateTime.Now).Days <= ParametrosRegraAgendamento.MinimoDiaAntecedenciaAgendamento)
            {
                throw new InformacaoException($"A reunião deve ser agendada com no mínimo {ParametrosRegraAgendamento.MinimoDiaAntecedenciaAgendamento} dia de antecedência", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataInicio.Subtract(DateTime.Now).Days >= ParametrosRegraAgendamento.MaximoDiaAntecedenciaAgendamento)
            {
                throw new InformacaoException($"A reunião deve ser agendada com no maximo {ParametrosRegraAgendamento.MaximoDiaAntecedenciaAgendamento} dias de antecedência", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataInicio.DayOfWeek == DayOfWeek.Saturday || informacoesAgendamentoSala.DataInicio.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new InformacaoException("A reunião só pode ser agendada em dias úteis.", InformacoesLog);
            }
        }
    }
}
