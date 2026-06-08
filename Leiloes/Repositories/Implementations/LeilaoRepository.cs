using AstraAiDotnet.Data;
using AstraAiDotnet.Leiloes.Models;
using AstraAiDotnet.Leiloes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstraAiDotnet.Leiloes.Repositories.Implementations
{
    public class LeilaoRepository : ILeilaoRepository
    {
        private readonly AppDbContext _context;

        public LeilaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarLeilaoAsync(Leilao leilao)
        {
            await _context.Leiloes.AddAsync(leilao);
            await SalvarAsync();
        }

        public async Task DeleteLeilaoAsync(long idLeilao)
        {
            var leilao = await _context.Leiloes.FindAsync(idLeilao);
            if (leilao != null)
            {
                _context.Leiloes.Remove(leilao);
                await SalvarAsync();
            }
        }

        public async Task<List<Leilao>> ListarLeiloesAsync()
        {
            return await _context.Leiloes.ToListAsync();
        }

        public async Task<Leilao?> ObterLeilaoPorIdAsync(long idLeilao)
        {
            return await _context.Leiloes.FindAsync(idLeilao);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeilaoAsync(Leilao leilao)
        {
            _context.Leiloes.Update(leilao);
            await SalvarAsync();
        }

        public async Task<IEnumerable<Leilao>> ListarLeiloesAbertosAsync()
        {
            return await _context.Leiloes
                .Where(l => l.StatusLeilao == "Aberto")
                .ToListAsync();
        }
    }
}