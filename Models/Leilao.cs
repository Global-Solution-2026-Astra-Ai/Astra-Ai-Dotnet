namespace AstraAiDotnet.Models
{
    public class Leilao
    {
        public long IdLeilao { get; private set; }
        public long IdSatelite { get; private set; }
        public long IdRcdennaOrigem { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public decimal GwhDisponivel { get; private set; }
        public decimal PrecoMinPorGwh { get; private set; }
        public string StatusLeilao { get; private set; }

        public ICollection<LogTransacao> LogTransacoes { get; set; } = new List<LogTransacao>();

        public Leilao() { }

        public Leilao(long idSatelite, long idRcdennaOrigem, DateTime dataHoraInicio, DateTime dataHoraFim, decimal gwhDisponivel, decimal precoMinPorGwh, string statusLeilao)
        {
            IdSatelite = idSatelite;
            IdRcdennaOrigem = idRcdennaOrigem;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            GwhDisponivel = gwhDisponivel;
            PrecoMinPorGwh = precoMinPorGwh;
            StatusLeilao = statusLeilao;
        }

        public void SetHoraInicio(DateTime horaInicio) => DataHoraInicio = horaInicio;
        public void SetHoraFim(DateTime novaHora) => DataHoraFim = novaHora;
        public void SetGwhDisponivel(decimal novoGwh) => GwhDisponivel = novoGwh;
        public void SetStatus(string novoStatus) => StatusLeilao = novoStatus;
    }
}
