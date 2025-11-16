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
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaDbContext _context;

        public AutorRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _context.Autores
                     .Include(m => m.Livros)
                     .AsNoTracking() // Usando para melhorar a performance em consultas somente leitura
                     .ToListAsync();
        }

        public async Task<Autor> GetByIdAsync(int id)
        {
            var autor = await _context.Autores
                                      .Include(m => m.Livros)
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(a => a.Id == id); // Encontra o primeiro com esse ID

            if (autor == null)
            {
                throw new KeyNotFoundException("Autor não encontrado");
            }

            return autor;
        }

        // Metodos de modificação de dados
        public async Task AddAsync(Autor autor)
        {
            await _context.Autores.AddAsync(autor);
        }

        public async Task UpdateAsync(Autor autor)
        {
            _context.Autores.Update(autor);
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                _context.Autores.Remove(autor);
            }
            else
            {
                throw new KeyNotFoundException("Autor não encontrado");
            }
        }

        public async Task<Autor> GetByIdParaAtualizacaoAsync(int id)
        {
            var autor = await _context.Autores
                .FirstOrDefaultAsync(u => u.Id == id);

            if (autor == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            return autor;
        }
    }
}
