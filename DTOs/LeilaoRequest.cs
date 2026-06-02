namespace AstraAiDotnet.DTOs
{
    public class LeilaoRequest
    {
        public long IdSatelite { get; set; }
        public long IdRcdennaOrigem { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public decimal GwhDisponivel { get; set; }
        public decimal PrecoMinPorGwh { get; set; }
        public string StatusLeilao { get; set; }
    }
}
