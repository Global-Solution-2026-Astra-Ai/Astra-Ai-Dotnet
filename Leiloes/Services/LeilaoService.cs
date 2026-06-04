using AstraAiDotnet.Leiloes.DTOs;
using AstraAiDotnet.Leiloes.Models;
using AstraAiDotnet.Leiloes.Repositories.Interfaces;
using AstraAiDotnet.LogTransacoes.DTOs;
using AstraAiDotnet.LogTransacoes.Services;

namespace AstraAiDotnet.Leiloes.Services
{
    public class LeilaoService
    {
        private readonly ILeilaoRepository _repository;
        private readonly LogTransacaoService _logTransacaoService;

        public LeilaoService(ILeilaoRepository repository, LogTransacaoService logTransacaoService)
        {
            _repository = repository;
            _logTransacaoService = logTransacaoService;
        }

        private static LeilaoResponse MapToResponse(Leilao leilao)
        {
            return new LeilaoResponse
            {
                IdLeilao = leilao.IdLeilao,
                IdSatelite = leilao.IdSatelite,
                IdRcdennaOrigem = leilao.IdRcdennaOrigem,
                DataHoraInicio = leilao.DataHoraInicio,
                DataHoraFim = leilao.DataHoraFim,
                GwhDisponivel = leilao.GwhDisponivel,
                PrecoMinPorGwh = leilao.PrecoMinPorGwh,
                StatusLeilao = leilao.StatusLeilao
            };
        }

        public async Task<List<LeilaoResponse>> ListarAsync()
        {
            var leiloes = await _repository.ListarLeiloesAsync();
            return leiloes.Select(MapToResponse).ToList();
        }

        public async Task<LeilaoResponse> ObterPorIdAsync(long idLeilao)
        {
            var leilao = await _repository.ObterLeilaoPorIdAsync(idLeilao);

            if (leilao is null)
            {
                throw new KeyNotFoundException("Leilão não encontrado.");
            }

            return MapToResponse(leilao);
        }

        public async Task<LeilaoResponse> CadastrarAsync(LeilaoRequest leilaoRequest)
        {
            ValidarDatasLeilao(leilaoRequest.DataHoraInicio, leilaoRequest.DataHoraFim);

            var leilao = new Leilao(
                leilaoRequest.IdSatelite,
                leilaoRequest.IdRcdennaOrigem,
                leilaoRequest.DataHoraInicio,
                leilaoRequest.DataHoraFim,
                leilaoRequest.GwhDisponivel,
                leilaoRequest.PrecoMinPorGwh,
                leilaoRequest.StatusLeilao
            );

            await _repository.AdicionarLeilaoAsync(leilao);

            return MapToResponse(leilao);
        }

        public async Task<LeilaoResponse> AtualizarAsync(long idLeilao, LeilaoRequest leilaoRequest)
        {
            var leilao = await _repository.ObterLeilaoPorIdAsync(idLeilao);

            if (leilao is null)
            {
                throw new KeyNotFoundException($"Leilão com ID {idLeilao} não encontrado.");
            }

            ValidarDatasLeilao(leilaoRequest.DataHoraInicio, leilaoRequest.DataHoraFim);

            leilao.SetHoraInicio(leilaoRequest.DataHoraInicio);
            leilao.SetHoraFim(leilaoRequest.DataHoraFim);
            leilao.SetGwhDisponivel(leilaoRequest.GwhDisponivel);
            leilao.SetStatus(leilaoRequest.StatusLeilao);

            await _repository.UpdateLeilaoAsync(leilao);

            return MapToResponse(leilao);
        }

        public async Task DeletarAsync(long idLeilao)
        {
            var leilao = await _repository.ObterLeilaoPorIdAsync(idLeilao);

            if (leilao is null)
            {
                throw new KeyNotFoundException($"Leilão com ID {idLeilao} não encontrado.");
            }

            await _repository.DeleteLeilaoAsync(idLeilao);
        }

        private static void ValidarDatasLeilao(DateTime dataInicio, DateTime dataFim)
        {
            if (dataFim <= dataInicio)
            {
                throw new ArgumentException("A data de término deve ser posterior à data de início.");
            }

            if (dataInicio < DateTime.Now)
            {
                throw new ArgumentException("A data de início não pode ser no passado.");
            }
        }

        public async Task<List<LeilaoResponse>> ListarAbertosAsync()
        {
            var leilosAbertos = await _repository.ListarLeiloesAbertosAsync();
            return leilosAbertos.Select(MapToResponse).ToList();
        }

        public async Task<LogTransacaoResponse> FinalizarLeilaoAsync(LogTransacaoRequest logTransacaoRequest)
        {
            var leilao = await _repository.ObterLeilaoPorIdAsync(logTransacaoRequest.IdLeilao);

            if ((leilao is null) || (leilao.IdLeilao != logTransacaoRequest.IdLeilao))
            {
                throw new KeyNotFoundException($"Leilão com ID {logTransacaoRequest.IdLeilao} não encontrado.");
            } else if (leilao.StatusLeilao != "Ativo")
            {
                throw new ArgumentException("Leilão não está ativo e não pode ser finalizado.");
            } else if (logTransacaoRequest.ValorArrematado < leilao.PrecoMinPorGwh)
            {
                throw new ArgumentException("Valor arrematado é inferior ao preço mínimo estipulado por GWh.");
            }

            leilao.SetStatus("Inativo");

            await _repository.UpdateLeilaoAsync(leilao);

            return await _logTransacaoService.RegistrarLogTransacaoAsync(logTransacaoRequest);
        }

    }
}