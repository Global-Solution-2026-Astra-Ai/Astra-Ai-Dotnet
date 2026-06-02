using AstraAiDotnet.DTOs;
using AstraAiDotnet.Models;
using AstraAiDotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace AstraAiDotnet.Controllers
{
    [ApiController]
    [Route("api/clientes-premium")]
    [Produces("application/json")]
    public class ClientePremiumController : ControllerBase
    {
        private readonly ClientePremiumService _service;

        public ClientePremiumController(ClientePremiumService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os clientes premium cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientePremiumResponse>>> Listar()
        {
            var clientes =
                await _service.ListarAsync();

            return Ok(clientes);
        }

        /// <summary>
        /// Busca cliente premium pelo ID
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientePremiumResponse>> ObterPorId(long id)
        {
            try
            {
                var cliente = await _service.ObterPorIdAsync(id);

                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(
                    new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cadastra um novo cliente premium
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar([FromBody] ClientePremiumRequest clienteRequest)
        {
            try
            {
                var clienteResponse = await _service.CadastrarAsync(clienteRequest);

                return CreatedAtAction(
                    nameof(ObterPorId),
                    new
                    {
                        id = clienteResponse.IdCliente
                    },
                    clienteResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados do cliente
        /// </summary>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(long id, [FromBody] ClientePremiumRequest clienteAtualizado)
        {
            try
            {
                await _service.UpdateAsync(id, clienteAtualizado);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza o status do cliente
        /// </summary>
        [HttpPatch("{id:long}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarStatus(long id, [FromBody] string novoStatus)
        {
            try
            {
                await _service.AtualizarStatusAsync(id, novoStatus);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um cliente premium
        /// </summary>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _service.DeletarAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}