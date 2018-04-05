using AtividadeLemafJoseRenato.Util.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor
{
    public class RequisicaoBase
    {
        public LogContexto InformacoesLog { get; set; }

        public RequisicaoBase(LogContexto contexto)
        {
            InformacoesLog = contexto;
        }
    }
}
