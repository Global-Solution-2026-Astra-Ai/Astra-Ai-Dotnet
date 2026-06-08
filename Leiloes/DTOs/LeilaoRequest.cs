namespace AstraAiDotnet.Leiloes.DTOs
{
    public class LeilaoRequest
    {
        public long IdSatelite { get; set; }
        public long IdRectennaOrigem { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public decimal GwhDisponivel { get; set; }
        public decimal PrecoMinPorGwh { get; set; }
        public string StatusLeilao { get; set; } = String.Empty;
    }
}
