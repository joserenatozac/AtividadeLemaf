using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace AtividadeLemafJoseRenato.Repositorios
{
    public class RepositorioBase
    {
        private void Teste()
        {
            /*A variável strcon é o connection string que copiamos anteriormente enquanto criávamos o banco de dados, essa variável poderia ser utilizada para todos os botões do programa, mas irei repeti-la várias vezes para fixar a idéia dos passos que precisamos seguir para fazer a conexão com o banco, Obs.: note que o caminho do seu banco precisa estar com “\\” se não estiver coloque */
            string strcon = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Documents and Settings\\k\\Meus documentos\\banco_dados.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            SqlConnection conexao = new SqlConnection(strcon); /* conexao irá conectar o C# ao banco de dados */
            SqlCommand cmd = new SqlCommand("SELECT * FROM tabela", conexao); /*cmd possui mais de um parâmetro, neste caso coloquei o comando SQL "SELECT * FROM tabela" que irá selecionar tudo(*) de tabela, o segundo parâmetro indica onde o banco está conectado,ou seja se estamos selecionando informações do banco precisamos dizer onde ele está localizado */
            Try
            {
                conexao.Open(); // abre a conexão com o banco   
                cmd.ExecuteNonQuery(); // executa cmd
                                       /*Pronto após o cmd.ExecuteNonQuery(); selecionamos tudo o que tinha dentro do banco, agora os passos seguintes irão exibir as informações para que o usuário possa vê-las    */
                SqlDataAdapter da = new SqlDataAdapter(); /* da, adapta o banco de dados ao nosso projeto */
                DataSet ds = new DataSet();
                da.SelectCommand = cmd; // adapta cmd ao projeto
                da.Fill(ds); // preenche todas as informações dentro do DataSet                                          
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                conexao.Close(); /* Se tudo ocorrer bem fecha a conexão com o banco da dados, sempre é bom fechar a conexão após executar até o final o que nos interessa, isso pode evitar problemas futuros */
            }
        }
    }
}
