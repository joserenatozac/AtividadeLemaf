using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Entidades
{
    public abstract class SalaEntidade
    {
        public virtual int IdSala { get; set; }
        public virtual int QuantidadePessoas { get; set; }
        public virtual bool TemAcessoInternet { get; set; }
        public virtual bool TemWebcamConferencia { get; set; }
        public virtual bool TemComputador { get; set; }
        public List<HistoricoSalaEntidade> HistoricosDeReunioes { get; set; }
    }
}
