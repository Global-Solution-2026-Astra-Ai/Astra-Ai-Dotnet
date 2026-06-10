using AstraAiDotnet.LogTransacoes.Models;
using AstraAiDotnet.LogTransacoes.Repositories.Interfaces;
using AstraAiDotnet.LogTransacoes.DTOs;

namespace AstraAiDotnet.LogTransacoes.Services
{
    public class LogTransacaoService
    {
        private readonly ILogTransacaoRepository _logTransacaoRepository;

        public LogTransacaoService(ILogTransacaoRepository logTransacaoRepository)
        {
            _logTransacaoRepository = logTransacaoRepository;
        }

        private static LogTransacaoResponse MapToResponse(LogTransacao logTransacao)
        {
            return new LogTransacaoResponse
            {
                IdTransacao = logTransacao.IdTransacao,
                IdLeilao = logTransacao.IdLeilao,
                IdClienteVencedor = logTransacao.IdClienteVencedor,
                ValorArrematado = logTransacao.ValorArrematado,
                TaxaAstra = logTransacao.TaxaAstra,
                DataFaturamento = logTransacao.DataFaturamento
            };
        }

        public async Task<LogTransacaoResponse> RegistrarLogTransacaoAsync(LogTransacaoRequest logTransacaoRequest)
        {
            var logTransacao = new LogTransacao(
                logTransacaoRequest.IdLeilao,
                logTransacaoRequest.IdClienteVencedor,
                logTransacaoRequest.ValorArrematado,
                DateTime.Now
            );

            await _logTransacaoRepository.RegistrarLogTransacaoAsync(logTransacao);

            return MapToResponse(logTransacao);
        }

        public async Task ExcluirLogTransacaoAsync(long idTransacao)
        {
            await _logTransacaoRepository.ExcluirLogTransacaoAsync(idTransacao);
        }

    }
}