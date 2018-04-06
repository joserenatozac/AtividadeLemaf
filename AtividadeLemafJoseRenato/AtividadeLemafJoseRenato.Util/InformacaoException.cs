using AtividadeLemafJoseRenato.Util.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Util
{
    public class InformacaoException : Exception
    {
        public LogContexto Contexto { get; set; }

        public InformacaoException(string mensagem, LogContexto contexto)
            : base(mensagem)
        {
            Contexto = contexto;
        }

        public InformacaoException(string mensagem, Exception excecaoInterna, LogContexto contexto)
            : base(mensagem, excecaoInterna)
        {
            Contexto = contexto;
        }
    }
}
