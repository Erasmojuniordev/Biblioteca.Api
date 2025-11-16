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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaDbContext _context;

        public UsuarioRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Emprestimos)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            
            var usuario = _context.Usuarios
                .Include(u => u.Emprestimos)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            return usuario;
        }

        public async Task<Usuario> GetByIdParaAtualizacaoAsync(int id)
        {
            // É igual ao GetByIdAsync, MAS SEM O .AsNoTracking()
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) { throw new Exception("Usuário não encontrado"); }
            _context.Usuarios.Remove(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }
    }
}
