using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Util.Log
{
    public class LogContexto
    {
        public string NomeFluxo { get; set; }
        public string NomeTransacao { get; set; }
        public string TipoLog { get; set; }

        public LogContexto(string nomeFluxo, string nomeTransacao)
        {
            NomeFluxo = nomeFluxo;
            NomeTransacao = nomeTransacao;
        }
    }
}
