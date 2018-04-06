using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Repositorios
{
    public interface IEnviarEmailRepositorio
    {
        bool EnviarEmail(string remetente, string destinatario, string assunto, string corpoEmail);
    }
}
