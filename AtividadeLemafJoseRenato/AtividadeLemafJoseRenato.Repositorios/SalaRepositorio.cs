using AtividadeLemafJoseRenato.Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtividadeLemafJoseRenato.Entidades;
using AtividadeLemafJoseRenato.Repositorios.EntidadesBd;
using System.Data.SQLite;

namespace AtividadeLemafJoseRenato.Repositorios
{
    public class SalaRepositorio : RepositorioBase, ISalaRepositorio
    {
        private static string COLUNAS_SALA = @"`id_sala`,
                                               `qtd_pessoas`,
                                               `idc_acesso_internet`,
                                               `idc_webcam_conferencia`,
                                               `idc_computador`
                                                ";

        private static string SQL_SELECT_SALA = $@"SELECT {COLUNAS_SALA} FROM `sala`";

        public List<SalaEntidade> ListarTodas()
        {
            List<SalaEntidade> listaSalas = new List<SalaEntidade>();
            using (var conexao = new SQLiteConnection(_stringConexao))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(conexao))
                {
                    comando.CommandText = SQL_SELECT_SALA;
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalaEntidadeBd sala = ObterSalaEntidadeAPartirReader(reader);
                            listaSalas.Add(sala);
                        }
                    }
                }
            }
            return listaSalas;
        }

        public SalaEntidade Obter(int idSala)
        {
            throw new NotImplementedException();
        }

        private SalaEntidadeBd ObterSalaEntidadeAPartirReader(SQLiteDataReader reader)
        {
            return new SalaEntidadeBd()
            {
                IdSala = Convert.ToInt32(reader["id_sala"]),
                QuantidadePessoas = Convert.ToInt32(reader["qtd_pessoas"]),
                TemAcessoInternet = Convert.ToBoolean(reader["idc_acesso_internet"]),
                TemWebcamConferencia = Convert.ToBoolean(reader["idc_webcam_conferencia"]),
                TemComputador = Convert.ToBoolean(reader["idc_computador"])
            };
        }
    }
}
