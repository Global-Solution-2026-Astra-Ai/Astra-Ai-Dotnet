using AstraAiDotnet.Leiloes.DTOs;
using AstraAiDotnet.Leiloes.Models;
using AstraAiDotnet.Leiloes.Repositories.Interfaces;

namespace AstraAiDotnet.Leiloes.Services
{
    public class LeilaoService
    {
        private readonly ILeilaoRepository _repository;

        public LeilaoService(ILeilaoRepository repository)
        {
            _repository = repository;
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

    }
}