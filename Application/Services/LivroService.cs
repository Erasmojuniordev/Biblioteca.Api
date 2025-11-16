using api_biblioteca.Middleware;
using Application.DTOs.Livro;
using Application.IServices;
using AutoMapper;
using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;
        private readonly BibliotecaDbContext _context;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public LivroService(ILivroRepository livroRepository, BibliotecaDbContext context, IEmprestimoRepository emprestimoRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
            _context = context;
            _emprestimoRepository = emprestimoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<Livro> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateLivroDto livroDto)
        {
            if (string.IsNullOrWhiteSpace(livroDto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            var novoLivro = _mapper.Map<Livro>(livroDto);

            // Lógica N:N: Buscar os autores e associar
            if (livroDto.AutorIds != null && livroDto.AutorIds.Any())
            {
                // Busca no banco os autores que existem com os IDs da lista
                var autores = await _context.Autores
                    .Where(a => livroDto.AutorIds.Contains(a.Id))
                    .ToListAsync();

                novoLivro.Autores = autores;
            }

            await _livroRepository.AddAsync(novoLivro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emprestimosAtivos = _emprestimoRepository.GetByLivroIdAsync(id,true);

            if (emprestimosAtivos.Result.Any())
            {
                throw new BusinessRuleException("Não é possível deletar o livro, existem empréstimos ativos associados a ele.");
            }

            await _livroRepository.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateLivroDto livroDto)
        {
            if (string.IsNullOrWhiteSpace(livroDto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            var livroParaAtualizar = await _livroRepository.GetByIdParaAtualizacaoAsync(livroDto.Id);

            livroParaAtualizar.Titulo = livroDto.Titulo;

            livroParaAtualizar.Autores.Clear();
            // Adiciona os novos autores
            if (livroDto.AutorIds != null && livroDto.AutorIds.Any())
            {
                var autores = await _context.Autores
                    .Where(a => livroDto.AutorIds.Contains(a.Id))
                    .ToListAsync();

                livroParaAtualizar.Autores = autores;
            }
            await _context.SaveChangesAsync();
        }
    }
}
