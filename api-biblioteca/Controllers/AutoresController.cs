using Application.DTOs.Autor;
using Application.IServices;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace api_biblioteca.Controllers
{
    [ApiController] // Define que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a URL: /api/autores
    public class AutoresController : ControllerBase
    {
        // 1. Injeção de Dependência (pedindo o "Serviço")
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var autores = await _autorService.GetAllAsync();
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAutorDto novoAutor)
        {
            try
            {
                await _autorService.AddAsync(novoAutor);
                return Ok();
            }
            catch (Exception ex)
            {
                // Retorna 400 Bad Request se a regra de negócio falhar
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAutorDto autorAtualizado)
        {
            if (id != autorAtualizado.Id)
            {
                return BadRequest("ID do autor não corresponde ao ID na URL.");
            }
            try
            {
                await _autorService.UpdateAsync(autorAtualizado);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _autorService.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
