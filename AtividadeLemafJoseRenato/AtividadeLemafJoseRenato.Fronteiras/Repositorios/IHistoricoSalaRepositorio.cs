using AtividadeLemafJoseRenato.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Repositorios
{
    public interface IHistoricoSalaRepositorio
    {
        List<HistoricoSalaEntidade> ListarAgendamentosFuturos(int idSala, DateTime dataInicio);
        List<HistoricoSalaEntidade> ListarSalasOcupadas(DateTime dataInicioReuniaoAgendar, DateTime dataFimReuniaoAgendar);
        void Inserir(int idSala, DateTime dataInicio, DateTime dataFim);
    }
}
