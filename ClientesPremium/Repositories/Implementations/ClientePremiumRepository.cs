using AstraAiDotnet.Data;
using AstraAiDotnet.ClientesPremium.Models;
using AstraAiDotnet.ClientesPremium.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstraAiDotnet.ClientesPremium.Repositories.Implementations
{
    public class ClientePremiumRepository : IClientePremiumRepository
    {
        private readonly AppDbContext _context;

        public ClientePremiumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientePremium>> ListarAsync()
        {
            return await _context.ClientesPremium.ToListAsync();
        }

        public async Task<ClientePremium?> ObterPorIdAsync(long idCliente)
        {
            return await _context.ClientesPremium.FindAsync(idCliente);
        }

        public async Task<ClientePremium?> ObterPorCnpjAsync(string cnpj)
        {
            return await _context.ClientesPremium.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
        }

        public async Task AdicionarAsync(ClientePremium cliente)
        {
            await _context.ClientesPremium.AddAsync(cliente);
            await SalvarAsync();
        }

        public async Task UpdateAsync(ClientePremium cliente)
        {
            _context.ClientesPremium.Update(cliente);
            await SalvarAsync();
        }

        public async Task DeleteAsync(long idCliente)
        {
            var cliente = await _context.ClientesPremium.FindAsync(idCliente);
            if (cliente is not null)
            {
                _context.ClientesPremium.Remove(cliente);
                await SalvarAsync();
            }
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
