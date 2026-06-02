namespace AstraAiDotnet.DTOs
{
    public class LogTransacaoRequest
    {
        public long IdLeilao { get; set; }
        public long IdClienteVencedor { get; set; }
        public decimal ValorArrematado { get; set; }
        public DateTime DataFaturamento { get; set; }
    }
}
