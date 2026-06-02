namespace AstraAiDotnet.DTOs
{
    public class LogTransacaoResponse
    {
        public long IdTransacao { get; set; }
        public long IdLeilao { get; set; }
        public long IdClienteVencedor { get; set; }
        public decimal ValorArrematado { get; set; }
        public decimal TaxaAstra { get; set; }
        public DateTime DataFaturamento { get; set; }
    }
}
