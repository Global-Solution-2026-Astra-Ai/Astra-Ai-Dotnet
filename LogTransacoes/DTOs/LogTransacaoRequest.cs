namespace AstraAiDotnet.LogTransacoes.DTOs
{
    public class LogTransacaoRequest
    {
        public long IdLeilao { get; set; }
        public long IdClienteVencedor { get; set; }
        public decimal ValorArrematado { get; set; }
    }
}
