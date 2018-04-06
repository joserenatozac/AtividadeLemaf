using AtividadeLemafJoseRenato.Fronteiras.Dtos;
using System.Collections.Generic;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao
{
    public class SelecionarOuSugerirSalaResultado : ResultadoBase
    {
        public int? IdSalaAgendada { get; set; }
        public List<AgendamentoSugestaoDto> ListaSugestoesAgendamentos { get; set; }
    }
}
