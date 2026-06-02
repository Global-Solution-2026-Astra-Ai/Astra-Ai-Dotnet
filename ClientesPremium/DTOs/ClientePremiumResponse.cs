namespace AstraAiDotnet.ClientesPremium.DTOs
{
    public class ClientePremiumResponse
    {
        public long IdCliente { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public decimal DemandaContratadaGwh { get; set; }
        public string StatusCadastro { get; set; }
    }
}
