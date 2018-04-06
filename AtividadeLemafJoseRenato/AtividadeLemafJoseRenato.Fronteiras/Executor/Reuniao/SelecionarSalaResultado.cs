using AtividadeLemafJoseRenato.Fronteiras.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao
{
    public class SelecionarSalaResultado : ResultadoBase
    {
        public int? IdSalaAgendada { get; set; }
        public List<AgendamentoSugestaoDto> ListaSugestoesAgendamentos { get; set; }
    }
}
