using Application.DTOs.Autor;
using Application.DTOs.Usuario;
using Application.IServices;
using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly BibliotecaDbContext _context; // Prática errada, refatorar depois com IUnitOfWork

        public AutorService(IAutorRepository autorRepository, BibliotecaDbContext context)
        {
            _autorRepository = autorRepository;
            _context = context;
        }
        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _autorRepository.GetAllAsync();
        }

        public async Task<Autor> GetByIdAsync(int id)
        {
            return await _autorRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(CreateAutorDto autorDto)
        {
            if (string.IsNullOrWhiteSpace(autorDto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            var novoAutor = new Autor
            {
                Nome = autorDto.Nome
            };

            await _autorRepository.AddAsync(novoAutor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);

            if (autor.Livros.Any())
            {
                throw new Exception("Não é possível excluir autor. Exclua os livros dele primeiro.");
            }

            await _autorRepository.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAutorDto autorDto)
        {
            if (string.IsNullOrWhiteSpace(autorDto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            // Busca a entidade para atualizar (sem AsNoTracking!)
            var autorParaAtualizar = await _autorRepository.GetByIdParaAtualizacaoAsync(autorDto.Id);

            // Mapeamento: DTO -> Entidade
            autorParaAtualizar.Nome = autorDto.Nome;

            await _autorRepository.UpdateAsync(autorParaAtualizar);
            await _context.SaveChangesAsync();
        }
    }
}
