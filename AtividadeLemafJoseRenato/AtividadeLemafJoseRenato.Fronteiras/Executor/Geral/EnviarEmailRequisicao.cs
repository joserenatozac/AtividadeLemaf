using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtividadeLemafJoseRenato.Util.Log;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor.Geral
{
    public class EnviarEmailRequisicao : RequisicaoBase
    {
        public string Remetente { get; set; }
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string CorpoEmail { get; set; }

        public EnviarEmailRequisicao(LogContexto contexto) : base(contexto)
        {
        }
    }
}
