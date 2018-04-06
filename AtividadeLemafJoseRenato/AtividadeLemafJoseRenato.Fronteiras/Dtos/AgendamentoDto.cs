using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fronteira.Dtos
{
    public class AgendamentoDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadePessoas { get; set; }
        public bool NecessitaAcessoInternet { get; set; }
        public bool NecessitaTvEWebcam { get; set; }
        public string EntradaBruta { get; set; }
    }
}
