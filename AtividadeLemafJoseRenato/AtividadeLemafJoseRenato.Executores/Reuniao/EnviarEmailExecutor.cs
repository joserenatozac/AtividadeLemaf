using AtividadeLemafJoseRenato.Fronteiras.Executor;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Geral;
using AtividadeLemafJoseRenato.Fronteiras.Repositorios;
using AtividadeLemafJoseRenato.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Executores.Reuniao
{
    public class EnviarEmailExecutor : ExecutorBase, IExecutor<EnviarEmailRequisicao, EnviarEmailResultado>
    {
        private readonly IEnviarEmailRepositorio enviarEmailRepositorio;
        
        public EnviarEmailExecutor()
        {
            enviarEmailRepositorio = new EnviarEmailRepositorio();
        }

        public EnviarEmailResultado Executar(EnviarEmailRequisicao requisicao)
        {
            enviarEmailRepositorio.EnviarEmail(requisicao.Remetente, requisicao.Destinatario,
                requisicao.Assunto, requisicao.CorpoEmail);
            return new EnviarEmailResultado();
        }
    }
}
