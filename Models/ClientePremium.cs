namespace AstraAiDotnet.Models
{
    public class ClientePremium
    {
        public long IdCliente { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public decimal DemandaContratadaGwh { get; private set; }
        public string StatusCadastro { get; private set; }

        //deixar readonly?
        public ICollection<LogTransacao> Transacoes { get; private set; } = new List<LogTransacao>();

        public ClientePremium() { }

        public ClientePremium(string razaoSocial, string cnpj, decimal demandaContratadaGwh, string statusCadastro)
        {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            DemandaContratadaGwh = demandaContratadaGwh;
            StatusCadastro = statusCadastro;
        }

        public void SetRazaoSocial(string novaRazaoSocial) => RazaoSocial = novaRazaoSocial;
        public void SetDemanda(decimal novaDemanda) => DemandaContratadaGwh = novaDemanda;
        public void SetStatus(string novoStatus) => StatusCadastro = novoStatus;

        public void AddTransacao(LogTransacao transacao)
        {
            Transacoes.Add(transacao);
        }
    }
}
