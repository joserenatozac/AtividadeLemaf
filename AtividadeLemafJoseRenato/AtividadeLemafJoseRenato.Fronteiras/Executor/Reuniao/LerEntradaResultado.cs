﻿using Fronteira.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao
{
    public class LerEntradaResultado : ResultadoBase
    {
        public List<AgendamentoDto> ListaInformacoesAgendamentoReuniao { get; set; }
    }
}
