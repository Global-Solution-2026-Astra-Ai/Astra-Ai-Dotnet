using AstraAiDotnet.ClientesPremium.DTOs;
using AstraAiDotnet.ClientesPremium.Models;
using AstraAiDotnet.ClientesPremium.Repositories.Interfaces;

namespace AstraAiDotnet.ClientesPremium.Services
{
    public class ClientePremiumService
    {
        private readonly IClientePremiumRepository _repository;

        public ClientePremiumService(IClientePremiumRepository repository)
        {
            _repository = repository;
        }

        private static ClientePremiumResponse MapToResponse(ClientePremium cliente)
        {
            return new ClientePremiumResponse
            {
                IdCliente = cliente.IdCliente,
                RazaoSocial = cliente.RazaoSocial,
                Cnpj = cliente.Cnpj,
                DemandaContratadaGwh = cliente.DemandaContratadaGwh,
                StatusCadastro = cliente.StatusCadastro
            };
        }

        public async Task<List<ClientePremiumResponse>> ListarAsync()
        {
            var clientes = await _repository.ListarAsync();
            return clientes.Select(MapToResponse).ToList();
        }

        public async Task<ClientePremiumResponse> ObterPorIdAsync(long idCliente)
        {
            var cliente = await _repository.ObterPorIdAsync(idCliente);

            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            return MapToResponse(cliente);
        }

        public async Task<ClientePremiumResponse> CadastrarAsync(ClientePremiumRequest clienteRequest)
        {
            var clienteExistente = await _repository.ObterPorCnpjAsync(clienteRequest.Cnpj);

            if (clienteExistente is not null)
            {
                throw new ArgumentException("Já existe cliente com esse CNPJ.");
            }

            var cliente = new ClientePremium(
                clienteRequest.RazaoSocial,
                clienteRequest.Cnpj,
                clienteRequest.DemandaContratadaGwh,
                clienteRequest.StatusCadastro
            );

            await _repository.AdicionarAsync(cliente);
            await _repository.SalvarAsync();

            return MapToResponse(cliente);
        }

        public async Task UpdateAsync(long idCliente, ClientePremiumRequest clienteAtualizado)
        {
            var cliente = await _repository.ObterPorIdAsync(idCliente);

            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            cliente.SetRazaoSocial(clienteAtualizado.RazaoSocial);
            cliente.SetDemanda(clienteAtualizado.DemandaContratadaGwh);
            cliente.SetStatus(clienteAtualizado.StatusCadastro);

            await _repository.UpdateAsync(cliente);

            await _repository.SalvarAsync();
        }

        public async Task AtualizarStatusAsync(long idCliente, string novoStatus)
        {
            var cliente = await _repository.ObterPorIdAsync(idCliente);

            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            cliente.SetStatus(novoStatus);

            await _repository.UpdateAsync(cliente);

            await _repository.SalvarAsync();
        }

        public async Task DeletarAsync(long idCliente)
        {
            var cliente = await _repository.ObterPorIdAsync(idCliente);

            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            await _repository.DeleteAsync(idCliente);
        }
    }
}