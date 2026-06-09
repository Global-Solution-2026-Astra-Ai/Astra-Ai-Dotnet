using AstraAiDotnet.LogTransacoes.Models;

namespace AstraAiDotnet.LogTransacoes.Repositories.Interfaces
{
    public interface ILogTransacaoRepository
        {
            Task RegistrarLogTransacaoAsync(LogTransacao logTransacao);

            Task ExcluirLogTransacaoAsync(long idTransacao);
        }

}