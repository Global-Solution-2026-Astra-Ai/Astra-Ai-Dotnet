using AstraAiDotnet.Data;
using AstraAiDotnet.LogTransacoes.Models;
using AstraAiDotnet.LogTransacoes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstraAiDotnet.LogTransacoes.Repositories.Implementations
{
    public class LogTransacaoRepository : ILogTransacaoRepository
    {
        private readonly AppDbContext _context;

        public LogTransacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarLogTransacaoAsync(LogTransacao logTransacao)
        {
            await _context.LogTransacoes.AddAsync(logTransacao);
            await _context.SaveChangesAsync();
        }
    }
}