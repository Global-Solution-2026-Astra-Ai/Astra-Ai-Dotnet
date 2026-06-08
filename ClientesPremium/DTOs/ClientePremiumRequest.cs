namespace AstraAiDotnet.ClientesPremium.DTOs
{
    public class ClientePremiumRequest
    {
        public string RazaoSocial { get; set; } = String.Empty;
        public string Cnpj { get; set; } = String.Empty;
        public decimal DemandaContratadaGwh { get; set; }
        public string StatusCadastro { get; set; } = String.Empty;
    }
}
