using AstraAiDotnet.Models;

namespace AstraAiDotnet.Repositories.Interfaces
{
    public interface IClientePremiumRepository
    {
        Task<List<ClientePremium>> ListarAsync();

        Task<ClientePremium?> ObterPorIdAsync(long idCliente);

        Task<ClientePremium?> ObterPorCnpjAsync(string cnpj);

        Task AdicionarAsync(ClientePremium cliente);

        Task UpdateAsync(ClientePremium cliente);

        Task DeleteAsync(long idCliente);

        Task SalvarAsync();
    }
}