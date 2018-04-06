using AtividadeLemafJoseRenato.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Repositorios
{
    public interface ISalaRepositorio
    {
        SalaEntidade Obter(int idSala);
        List<SalaEntidade> ListarTodas();
    }
}
