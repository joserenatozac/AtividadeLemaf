using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor
{
    public interface IExecutor<TRequisicao, TResultado> where TResultado : ResultadoBase
    {
        TResultado Executar(TRequisicao requisicao);
    }
}
