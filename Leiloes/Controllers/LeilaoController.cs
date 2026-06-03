using AstraAiDotnet.Leiloes.DTOs;
using AstraAiDotnet.Leiloes.Services;
using Microsoft.AspNetCore.Mvc;

namespace AstraAiDotnet.Leiloes.Controllers
{
    [ApiController]
    [Route("api/leiloes")]
    [Produces("application/json")]
    public class LeilaoController : ControllerBase
    {
        private readonly LeilaoService _service;

        public LeilaoController(LeilaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os leilões cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<LeilaoResponse>>> Listar()
        {
            var leiloes = await _service.ListarAsync();

            return Ok(leiloes);
        }

        /// <summary>
        /// Busca um leilão pelo ID
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeilaoResponse>> ObterPorId(long id)
        {
            try
            {
                var leilao = await _service.ObterPorIdAsync(id);

                return Ok(leilao);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cadastra um novo leilão
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/leiloes
        /// {
        ///     "idSatelite": 1,
        ///     "idRcdennaOrigem": 10,
        ///     "dataHoraInicio": "2026-06-10T08:00:00",
        ///     "dataHoraFim": "2026-06-10T18:00:00",
        ///     "gwhDisponivel": 150.5,
        ///     "precoMinPorGwh": 200.75,
        ///     "statusLeilao": "ABERTO"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar(
            [FromBody] LeilaoRequest leilaoRequest)
        {
            try
            {
                var leilaoResponse =
                    await _service.CadastrarAsync(leilaoRequest);

                return CreatedAtAction(
                    nameof(ObterPorId),
                    new
                    {
                        id = leilaoResponse.IdLeilao
                    },
                    leilaoResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um leilão existente
        /// </summary>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeilaoResponse>> Atualizar(
            long id,
            [FromBody] LeilaoRequest leilaoAtualizado)
        {
            try
            {
                var leilao =
                    await _service.AtualizarAsync(id, leilaoAtualizado);

                return Ok(leilao);
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
        /// Deleta um leilão
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