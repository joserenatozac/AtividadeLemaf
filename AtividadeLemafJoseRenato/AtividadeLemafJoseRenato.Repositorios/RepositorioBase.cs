using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Configuration;
using System.Data.SQLite;

namespace AtividadeLemafJoseRenato.Repositorios
{
    public class RepositorioBase
    {
        protected string _stringConexao;

        public RepositorioBase()
        {
            _stringConexao = ConfigurationManager.ConnectionStrings["reuniao"].ConnectionString;
        }
    }
}
