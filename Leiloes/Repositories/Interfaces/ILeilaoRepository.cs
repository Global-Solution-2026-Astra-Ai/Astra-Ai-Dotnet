using AstraAiDotnet.Leiloes.Models;

namespace AstraAiDotnet.Leiloes.Repositories.Interfaces
{
    public interface ILeilaoRepository
    {
        Task<List<Leilao>> ListarLeiloesAsync();

        Task<Leilao?> ObterLeilaoPorIdAsync(long idLeilao);

        Task AdicionarLeilaoAsync(Leilao leilao);

        Task UpdateLeilaoAsync(Leilao leilao);

        Task DeleteLeilaoAsync(long idLeilao);

        Task SalvarAsync();
    }
}