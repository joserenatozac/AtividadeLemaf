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
using AtividadeLemafJoseRenato.Fronteiras.Dtos;

namespace AtividadeLemafJoseRenato.Executores.Reuniao
{
    public class SelecionarOuSugerirSalaExecutor : ExecutorBase, IExecutor<SelecionarOuSugerirSalaRequisicao, SelecionarOuSugerirSalaResultado>
    {
        private readonly ISalaRepositorio salaRepositorio;
        private readonly IHistoricoSalaRepositorio historicoSalaRepositorio;

        private List<SalaEntidade> ListaSalas { get; set; }
        private List<SalaEntidade> SalasIdeias { get; set; }
        private List<SalaEntidade> SalasSubutilizadas { get; set; }

        public SelecionarOuSugerirSalaExecutor()
        {
            salaRepositorio = new SalaRepositorio();
            historicoSalaRepositorio = new HistoricoSalaRepositorio();
        }

        public SelecionarOuSugerirSalaResultado Executar(SelecionarOuSugerirSalaRequisicao requisicao)
        {
            SelecionarOuSugerirSalaResultado resultado = new SelecionarOuSugerirSalaResultado();
            InformacoesLog = requisicao.InformacoesLog;
            ValidarInformacoesAgendamento(requisicao.InformacoesAgendamentoSala);
            ListaSalas = salaRepositorio.ListarTodas();
            resultado.IdSalaAgendada = SelecionarSala(requisicao);
            if (!resultado.IdSalaAgendada.HasValue)
                resultado.ListaSugestoesAgendamentos = SugerirSala(requisicao);
            return resultado;
        }

        private List<AgendamentoSugestaoDto> SugerirSala(SelecionarOuSugerirSalaRequisicao requisicao)
        {
            List<AgendamentoSugestaoDto> listaSugestoes = new List<AgendamentoSugestaoDto>();
            List<SalaEntidade> listaSalasComAgendamentosFuturos = salaRepositorio.ListarTodas();
            listaSalasComAgendamentosFuturos.ForEach(
                sala => sala.HistoricosDeReunioes = historicoSalaRepositorio.ListarAgendamentosFuturos(sala.IdSala,
                requisicao.InformacoesAgendamentoSala.DataInicio));

            listaSalasComAgendamentosFuturos.ForEach(sala =>
            {
                if (VerificarSalaIdeal(requisicao.InformacoesAgendamentoSala, sala))
                {
                    listaSugestoes.Add(ObterProximoHorarioVago(sala.HistoricosDeReunioes, requisicao.InformacoesAgendamentoSala));
                };
            });

            return listaSugestoes.OrderBy(sugestao => sugestao.DataInicioSugerida).ToList().GetRange(0, ParametrosRegraAgendamento.NumeroSugestoes);
        }

        private AgendamentoSugestaoDto ObterProximoHorarioVago(List<HistoricoSalaEntidade> historicosDeReunioes, AgendamentoDto informacoesAgendamentoSala)
        {
            double intervaloReuniao = informacoesAgendamentoSala.DataFim.Subtract(informacoesAgendamentoSala.DataInicio).TotalHours;
            HistoricoSalaEntidade[] historicosDeReunioesVetor = historicosDeReunioes.OrderBy(historico => historico.DataInicio).ToArray();
            int i;
            for(i = 0; i < historicosDeReunioes.Count - 1; i++)
            {
                if ((historicosDeReunioesVetor[i + 1].DataInicio - historicosDeReunioesVetor[i].DataFim).TotalHours > intervaloReuniao)
                    return new AgendamentoSugestaoDto()
                    {
                        IdSalaSugerida = historicosDeReunioesVetor[0].IdSala,
                        DataInicioSugerida = historicosDeReunioesVetor[0].DataFim.AddMinutes(1),
                        DataFimSugerida = historicosDeReunioesVetor[0].DataFim.AddHours(intervaloReuniao)
                    };
            }
            return new AgendamentoSugestaoDto()
            {
                IdSalaSugerida = historicosDeReunioesVetor[historicosDeReunioes.Count - 1].IdSala,
                DataInicioSugerida = historicosDeReunioesVetor[historicosDeReunioes.Count - 1].DataFim.AddMinutes(1),
                DataFimSugerida = historicosDeReunioesVetor[historicosDeReunioes.Count - 1].DataFim.AddHours(intervaloReuniao)
            };

        }

        private int? SelecionarSala(SelecionarOuSugerirSalaRequisicao requisicao)
        {
            int? retorno;
            List<SalaEntidade> salasDisponiveisNoHorario = ListarSalasDisponiveisNoHorario(requisicao, ListaSalas);
            SalasIdeias = BuscarSalasIdeais(requisicao.InformacoesAgendamentoSala, salasDisponiveisNoHorario);
            if (!SalasIdeias.Any())
            {
                SalasSubutilizadas = BuscarSubutilizadas(requisicao.InformacoesAgendamentoSala, salasDisponiveisNoHorario);
                retorno = SalasSubutilizadas.OrderBy(sala => sala.QuantidadePessoas).ThenBy(sala => sala.IdSala).FirstOrDefault()?.IdSala;
            }
            else
            {
                retorno = SalasIdeias.OrderBy(sala => sala.QuantidadePessoas).ThenBy(sala => sala.IdSala).FirstOrDefault()?.IdSala;
            }
            if (retorno.HasValue)
                RealizarAgendamento(retorno.Value, requisicao.InformacoesAgendamentoSala.DataInicio, requisicao.InformacoesAgendamentoSala.DataFim);
            return retorno;
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
            return salasDisponiveisNoHorario.Where(sala => VerificarSalaIdeal(informacoesAgendamentoSala, sala)).ToList();
        }

        private bool VerificarSalaIdeal(AgendamentoDto informacoesAgendamentoSala, SalaEntidade sala)
        {
            return sala.TemAcessoInternet == informacoesAgendamentoSala.NecessitaAcessoInternet
                            && sala.TemWebcamConferencia == informacoesAgendamentoSala.NecessitaTvEWebcam && sala.QuantidadePessoas >= informacoesAgendamentoSala.QuantidadePessoas;
        }

        private List<SalaEntidade> ListarSalasDisponiveisNoHorario(SelecionarOuSugerirSalaRequisicao requisicao, List<SalaEntidade> listaSalas)
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
