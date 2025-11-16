using api_biblioteca.Middleware;
using Application.DTOs.Autor;
using Application.DTOs.Usuario;
using Application.IServices;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly BibliotecaDbContext _context; // Prática errada, refatorar depois com IUnitOfWork

        public AutorService(IAutorRepository autorRepository, BibliotecaDbContext context, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _context = context;
            _mapper = mapper;
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

            // Utilizando AutoMapper para mapear DTO para Entidade
            var novoAutor = _mapper.Map<Autor>(autorDto);

            await _autorRepository.AddAsync(novoAutor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);

            if (autor.Livros.Any())
            {
                throw new BusinessRuleException("Não é possível excluir autor. Exclua os livros dele primeiro.");
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

            _mapper.Map(autorDto, autorParaAtualizar);

            await _autorRepository.UpdateAsync(autorParaAtualizar);
            await _context.SaveChangesAsync();
        }
    }
}
