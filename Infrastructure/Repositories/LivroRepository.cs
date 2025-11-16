using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        public readonly BibliotecaDbContext _context;
        public LivroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _context.Livros
                .Include(l => l.Autores)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Livro> GetByIdAsync(int id)
        {
            var livro = _context.Livros
                .Include(l => l.Autores)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null){throw new Exception("Livro não encontrado");}

            return livro;
        }

        public async Task<Livro> GetByIdParaAtualizacaoAsync(int id)
        {

            var livro = await _context.Livros
                .Include(l => l.Autores)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }

            return livro; 
        }

        public async Task AddAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null) { throw new Exception("Livro não encontrado"); }
            _context.Livros.Remove(livro);
        }

        public async Task UpdateAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
        }
    }
}
