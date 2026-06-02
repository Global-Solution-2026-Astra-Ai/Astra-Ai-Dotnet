namespace AstraAiDotnet.DTOs
{
    public class ClientePremiumRequest
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public decimal DemandaContratadaGwh { get; set; }
        public string StatusCadastro { get; set; }
    }
}
