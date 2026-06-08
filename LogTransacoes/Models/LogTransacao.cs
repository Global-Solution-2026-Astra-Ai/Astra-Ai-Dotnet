using AstraAiDotnet.ClientesPremium.Models;
using AstraAiDotnet.Leiloes.Models;

namespace AstraAiDotnet.LogTransacoes.Models
{
    public class LogTransacao
    {
        public long IdTransacao { get; private set; }
        public long IdLeilao { get; private set; }
        public long IdClienteVencedor { get; private set; }
        public decimal ValorArrematado { get; private set; }
        public decimal TaxaAstra { get; private set; }
        public DateTime DataFaturamento { get; private set; }

        public Leilao Leilao { get; set; } = null!;
        public ClientePremium ClienteVencedor { get; set; } = null!;

        public LogTransacao() { }

        public LogTransacao(long idLeilao, long idClienteVencedor, decimal valorArrematado, DateTime dataFaturamento)
        {
            IdLeilao = idLeilao;
            IdClienteVencedor = idClienteVencedor;
            ValorArrematado = valorArrematado;
            TaxaAstra = CalcularTaxaAstra();
            DataFaturamento = dataFaturamento;
        }

        public void SetValorArrematado(decimal novoValor) => ValorArrematado = novoValor;

        public void SetDataFaturamento(DateTime novaData) => DataFaturamento = novaData;
        public decimal CalcularTaxaAstra()
        {
            return ValorArrematado * 0.05m;
        }
    }
}
