using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtividadeLemafJoseRenato.Util.Log;
using Fronteira.Dtos;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao
{
    public class SelecionarOuSugerirSalaRequisicao : RequisicaoBase
    {
        public AgendamentoDto InformacoesAgendamentoSala { get; set; }

        public SelecionarOuSugerirSalaRequisicao(LogContexto contexto) : base(contexto)
        {
        }
    }
}
