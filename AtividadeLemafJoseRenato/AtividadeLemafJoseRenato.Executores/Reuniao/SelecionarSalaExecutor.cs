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

namespace AtividadeLemafJoseRenato.Executores.Reuniao
{
    public class SelecionarSalaExecutor : ExecutorBase, IExecutor<SelecionarSalaRequisicao, SelecionarSalaResultado>
    {
        public SelecionarSalaResultado Executar(SelecionarSalaRequisicao requisicao)
        {
            InformacoesLog = requisicao.InformacoesLog;
            ValidarInformacoesAgendamento(requisicao.InformacoesAgendamentoSala);
            List<SalaEntidade> listaSalas = new SalaRepositorio().ListarTodas();
            return null;
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
            if (informacoesAgendamentoSala.DataInicio.Subtract(DateTime.Now).Days >= ParametrosRegraAgendamento.MinimoDiaAntecedenciaAgendamento)
            {
                throw new InformacaoException($"A reunião deve ser agendada com no mínimo {ParametrosRegraAgendamento.MinimoDiaAntecedenciaAgendamento} dia de antecedência", InformacoesLog);
            }
            if (informacoesAgendamentoSala.DataInicio.Subtract(DateTime.Now).Days <= ParametrosRegraAgendamento.MaximoDiaAntecedenciaAgendamento)
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
