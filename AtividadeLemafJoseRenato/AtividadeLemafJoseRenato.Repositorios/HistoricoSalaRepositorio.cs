using AtividadeLemafJoseRenato.Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtividadeLemafJoseRenato.Entidades;
using AtividadeLemafJoseRenato.Repositorios.EntidadesBd;
using System.Data.SQLite;
using System.Data;

namespace AtividadeLemafJoseRenato.Repositorios
{
    public class HistoricoSalaRepositorio : RepositorioBase, IHistoricoSalaRepositorio
    {
        private static string COLUNAS_SALA_HIST = @"`id_sala`,
                                                    `dt_inicio`,
                                                    `dt_fim`,
                                                    `dt_agendamento`
                                                    ";

        private static string SQL_SELECT_SALA_HIST = $@"SELECT {COLUNAS_SALA_HIST} FROM `sala_hist` ";

        private static string SQL_WHERE_SALAS_OCUPADAS = @" WHERE (`dt_inicio` <= '{0}' AND `dt_fim` >= '{0}') OR (`dt_inicio` < '{1}' AND `dt_fim` > '{1}')";

        private static string SQL_INSERT_SALA_HIST = $@"INSERT INTO `sala_hist` ({COLUNAS_SALA_HIST}) VALUES ";

        private static string SQL_INSERT_VALUES = " ({0},'{1}','{2}','{3}')";

        private static string SQL_WHERE_AGENDAMENTOS_FUTUROS = @" WHERE (`id_sala` = @id_sala AND `dt_inicio` >= @dt_inicio";

        public List<HistoricoSalaEntidade> ListarSalasOcupadas(DateTime dataInicioReuniaoAgendar, DateTime dataFimReuniaoAgendar)
        {
            List<HistoricoSalaEntidade> listaHistoricosSala = new List<HistoricoSalaEntidade>();

            string query = string.Format(SQL_SELECT_SALA_HIST + SQL_WHERE_SALAS_OCUPADAS, dataInicioReuniaoAgendar.ToString("yyyy-MM-dd HH:mm:ss"),
                dataFimReuniaoAgendar.ToString("yyyy-MM-dd HH:mm:ss"));
            using (var conexao = new System.Data.SQLite.SQLiteConnection(_stringConexao))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(conexao))
                {
                    comando.CommandText = query;
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistoricoSalaEntidadeBd sala = ObterHistoricoSalaEntidadeAPartirReader(reader);
                            listaHistoricosSala.Add(sala);
                        }
                    }
                }
                return listaHistoricosSala;
            }
        }

        public void Inserir(int idSala, DateTime dataInicio, DateTime dataFim)
        {
            string query = string.Format(SQL_INSERT_SALA_HIST + SQL_INSERT_VALUES, idSala, dataInicio.ToString("yyyy-MM-dd HH:mm:ss"),
                dataFim.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            using (var conexao = new SQLiteConnection(_stringConexao))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(conexao))
                {
                    comando.CommandText = query;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<HistoricoSalaEntidade> ListarAgendamentosFuturos(int idSala, DateTime dataInicio)
        {
            List<HistoricoSalaEntidade> listaHistoricosSala = new List<HistoricoSalaEntidade>();

            string query = string.Format(SQL_SELECT_SALA_HIST + SQL_WHERE_AGENDAMENTOS_FUTUROS);
            using (var conexao = new SQLiteConnection(_stringConexao))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(conexao))
                {
                    comando.CommandText = query;
                    comando.CommandType = CommandType.Text;
                    comando.Parameters.Add(new SQLiteParameter("@id_sala", DbType.Int32) { Value = idSala });
                    comando.Parameters.Add(new SQLiteParameter("@dt_inicio", DbType.DateTime) { Value = dataInicio });
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistoricoSalaEntidadeBd sala = ObterHistoricoSalaEntidadeAPartirReader(reader);
                            listaHistoricosSala.Add(sala);
                        }
                    }
                }
            }
            return listaHistoricosSala;
        }

        private HistoricoSalaEntidadeBd ObterHistoricoSalaEntidadeAPartirReader(SQLiteDataReader reader)
        {
            return new HistoricoSalaEntidadeBd()
            {
                IdSala = Convert.ToInt32(reader["id_sala"]),
                DataInicio = Convert.ToDateTime(reader["dt_inicio"]),
                DataFim = Convert.ToDateTime(reader["dt_fim"]),
                DataAgendamento = Convert.ToDateTime(reader["dt_agendamento"])
            };
        }
    }
}
