namespace AstraAiDotnet.Models
{
    public class LogTransacao
    {
        public long IdTransacao { get; private set; }
        public long IdLeilao { get; private set; }
        public long IdClienteVencedor { get; private set; }
        public decimal ValorArrematado { get; private set; }
        public decimal TaxaAstra { get; private set; }
        public DateTime DataFaturamento { get; private set; }

        public Leilao Leilao { get; set; }
        public ClientePremium ClienteVencedor { get; set; }

        public LogTransacao() { }

        public LogTransacao(long idLeilao, long idClienteVencedor, decimal valorArrematado, decimal taxaAstra, DateTime dataFaturamento)
        {
            IdLeilao = idLeilao;
            IdClienteVencedor = idClienteVencedor;
            ValorArrematado = valorArrematado;
            TaxaAstra = taxaAstra;
            DataFaturamento = dataFaturamento;
        }

        public void SetValorArrematado(decimal novoValor) => ValorArrematado = novoValor;
        public void SetTaxaAstra(decimal novaTaxa) => TaxaAstra = novaTaxa;
        public void SetDataFaturamento(DateTime novaData) => DataFaturamento = novaData;
    }
}
