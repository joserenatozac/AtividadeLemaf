using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Entidades
{
    public abstract class HistoricoSalaEntidade
    {
        public virtual int IdSala { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual DateTime? DataAgendamento { get; set; }
    }
}
