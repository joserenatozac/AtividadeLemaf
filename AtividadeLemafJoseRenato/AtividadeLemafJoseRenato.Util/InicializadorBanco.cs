using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Util
{
    public static class InicializadorBanco
    {
        public static void InicializaBanco()
        {
            try
            {
                using (var conexao = new System.Data.SQLite.SQLiteConnection(ConfigurationManager.ConnectionStrings["reuniao"].ConnectionString))
                {
                    conexao.Open();
                    using (var comando = new System.Data.SQLite.SQLiteCommand(conexao))
                    {
                        comando.CommandText = @"DROP TABLE IF EXISTS `sala`";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"CREATE TABLE `sala`
                            (
	                            `id_sala`	INTEGER,
	                            `qtd_pessoas`	INTEGER,
	                            `idc_acesso_internet`	INTEGER,
	                            `idc_webcam_conferencia`	INTEGER,
	                            `idc_computador`	INTEGER,
	                            PRIMARY KEY(`id_sala`)
                            );";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"DROP TABLE IF EXISTS `sala_hist`";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"CREATE TABLE `sala_hist`
                            (
	                            `id_sala`	INTEGER,
	                            `dt_inicio`	DATETIME,
	                            `dt_fim`	DATETIME,
                                'dt_agendamento' DATETIME,
	                            PRIMARY KEY(`id_sala`,`dt_inicio`,`dt_fim`)
                            );";
                        comando.ExecuteNonQuery();
                    }
                    using (var comando = new System.Data.SQLite.SQLiteCommand(conexao))
                    {
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (1, 10, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (2, 10, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (3, 10, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (4, 10, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (5, 10, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (6, 10, 1, 0, 0)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (7, 10, 1, 0, 0)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (8, 3, 1, 1, 1)";
                        comando.ExecuteNonQuery();

                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (9, 3, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (10, 3, 1, 1, 1)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (11, 20, 0, 0, 0)";
                        comando.ExecuteNonQuery();
                        comando.CommandText = @"INSERT INTO `sala` (`id_sala`, `qtd_pessoas`, `idc_acesso_internet`, `idc_webcam_conferencia`, `idc_computador`)
                                                VALUES (12, 20, 0, 0, 0)";
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
