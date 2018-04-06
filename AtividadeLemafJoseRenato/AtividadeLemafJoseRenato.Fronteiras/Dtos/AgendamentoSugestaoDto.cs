using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Dtos
{
    public class AgendamentoSugestaoDto
    {
        public DateTime DataInicioSugerida { get; set; }
        public DateTime DataFimSugerida { get; set; }
        public int IdSalaSugerida { get; set; }
    }
}
