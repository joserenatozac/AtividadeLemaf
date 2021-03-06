﻿using AtividadeLemafJoseRenato.Fronteiras.Executor;
using AtividadeLemafJoseRenato.Fronteiras.Executor.Reuniao;
using AtividadeLemafJoseRenato.Util;
using AtividadeLemafJoseRenato.Util.Log;
using Fronteira.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Executores.Reuniao
{
    public class LerEntradaExecutor : ExecutorBase, IExecutor<LerEntradaRequisicao, LerEntradaResultado>
    {
        private readonly string Sim = "SIM";

        public LerEntradaResultado Executar(LerEntradaRequisicao requisicao)
        {
            InformacoesLog = requisicao.InformacoesLog;

            string[] linhasEntrada = File.ReadAllLines(requisicao.CaminhoArquivoEntrada);
            ValidarLinhas(linhasEntrada);
            List<AgendamentoDto> listaAgendamentoDto = linhasEntrada.Select(linha => ObterAgendamentoAPartirEntrada(linha)).ToList();
            return new LerEntradaResultado()
            {
                ListaInformacoesAgendamentoReuniao = listaAgendamentoDto
            };
        }

        private AgendamentoDto ObterAgendamentoAPartirEntrada(string linhaEntrada)
        {
            string[] parametros = linhaEntrada.Split(ParametrosEntradaAgendamento.SeparadorParametrosEntrada);
            ValidarDataEntrada(parametros[0]);
            ValidarHoraEntrada(parametros[1]);
            ValidarDataEntrada(parametros[2]);
            ValidarHoraEntrada(parametros[3]);
            return new AgendamentoDto()
            {
                DataInicio = ObterDateTimeAtravesDataEHoraString(parametros[0], parametros[1]),
                DataFim = ObterDateTimeAtravesDataEHoraString(parametros[2], parametros[3]),
                QuantidadePessoas = int.Parse(parametros[4]),
                NecessitaAcessoInternet = parametros[5].ToUpperInvariant() == Sim,
                NecessitaTvEWebcam = parametros[6].ToUpperInvariant() == Sim,
                EntradaBruta = linhaEntrada
            };
        }

        private DateTime ObterDateTimeAtravesDataEHoraString(string dataString, string horaString)
        {
            string[] informacoesData = dataString.Split(ParametrosEntradaAgendamento.SeparadorParametrosData);
            string[] informacoesHora = horaString.Split(ParametrosEntradaAgendamento.SeparadorParametrosHora);
            return new DateTime(int.Parse(informacoesData[2]), int.Parse(informacoesData[1]), int.Parse(informacoesData[0]),
                int.Parse(informacoesHora[0]), int.Parse(informacoesHora[1]), 0);
        }

        private void ValidarHoraEntrada(string hora)
        {
            InformacoesLog.TipoLog = TipoLog.Informacao;
            if (hora.Split(ParametrosEntradaAgendamento.SeparadorParametrosHora).Count() != ParametrosEntradaAgendamento.NumeroParametrosHora)
            {
                throw new InformacaoException($"A hora {hora} foi passada no formato errado. O correto é HH:mm", InformacoesLog);
            }
        }

        private void ValidarDataEntrada(string data)
        {
            InformacoesLog.TipoLog = TipoLog.Informacao;
            if (data.Split(ParametrosEntradaAgendamento.SeparadorParametrosData).Count() != ParametrosEntradaAgendamento.NumeroParametrosData)
            {
                throw new InformacaoException($"A data {data} foi passada no formato errado. O correto é dd-MM-aaaa", InformacoesLog);
            }
        }

        private void ValidarLinhas(string[] linhasEntrada)
        {
            InformacoesLog.TipoLog = TipoLog.Informacao;
            if (linhasEntrada.Count() < 1)
            {
                throw new InformacaoException("Foi enviada uma entrada vazia.", InformacoesLog);
            }
            else 
            {
                int contaLinhas = 1;
                foreach(string linha in linhasEntrada)
                {
                    if (linha.Split(ParametrosEntradaAgendamento.SeparadorParametrosEntrada).Count() != ParametrosEntradaAgendamento.NumeroParametrosEntrada)
                        throw new InformacaoException($"A linha {contaLinhas} do arquivo contem o número errado de parâmetros esperados.", InformacoesLog);
                }
            }
        }
    }
}
