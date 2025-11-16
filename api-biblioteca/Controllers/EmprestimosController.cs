using Application.DTOs;
using Application.DTOs.Emprestimo;
using Application.IServices;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace api_biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;
        public EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var emprestimos = await _emprestimoService.GetAllAsync();
            return Ok(emprestimos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emprestimo = await _emprestimoService.GetByIdAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            return Ok(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmprestimoDto emprestimo)
        {
            try
            {
                await _emprestimoService.AddAsync(emprestimo);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> DevolverLivro(int id)
        {
            try
            {
                await _emprestimoService.DevolverLivroAsync(id);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _emprestimoService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
