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
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public readonly BibliotecaDbContext _context;

        public EmprestimoRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Emprestimo>> GetAllAsync()
        {
            return await _context.Emprestimos
                .Include(e => e.Livro)
                .Include(e => e.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Emprestimo> GetByIdAsync(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro)
                .Include(e => e.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id); 

            if (emprestimo == null){throw new KeyNotFoundException("Empréstimo não encontrado.");}
            return emprestimo;
        }

        public async Task<Emprestimo> GetByIdParaAtualizacaoAsync(int id)
        {
            var emprestimo = await _context.Emprestimos
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
            {
                throw new KeyNotFoundException("Empréstimo não encontrado");
            }

            return emprestimo;
        }

        public async Task<IEnumerable<Emprestimo>> GetByUsuarioIdAsync(int usuarioId, bool apenasAtivos)
        {
            // Cria a consulta
            var query = _context.Emprestimos.Where(e => e.UsuarioId == usuarioId);

            // Adiciona o filtro SE necessário
            if (apenasAtivos)
            {
                query = query.Where(e => e.Ativo == true);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Emprestimo>> GetByLivroIdAsync(int livroId, bool apenasAtivos)
        {
            var query = _context.Emprestimos.Where(e => e.LivroId == livroId);

            if (apenasAtivos)
            {
                query = query.Where(e => e.Ativo == true);
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task AddAsync(Emprestimo emprestimo)
        {
            await _context.Emprestimos.AddAsync(emprestimo);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _context.Emprestimos.Where(e => e.Id == id)
                .ExecuteDeleteAsync();
            } 
            catch 
            {
                throw new KeyNotFoundException("Emprestimo não encontrado");    
            }
        }
    }
}
