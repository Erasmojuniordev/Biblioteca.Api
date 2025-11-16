using Application.DTOs.Usuario;
using Application.IServices;
using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly BibliotecaDbContext _context;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, BibliotecaDbContext context, IEmprestimoRepository emprestimoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _context = context;
            _emprestimoRepository = emprestimoRepository;
        }
        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return _usuarioRepository.GetAllAsync();
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            return _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateUsuarioDto usuarioDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioDto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            var novoUsuario = new Usuario
            {
                Nome = usuarioDto.Nome
            };

            await _usuarioRepository.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emprestimosAtivos = _emprestimoRepository.GetByUsuarioIdAsync(id, true);

            if (emprestimosAtivos.Result.Any())
            {
                throw new InvalidOperationException("O usuário possui empréstimos ativos e não pode ser excluído.");
            }

            await _usuarioRepository.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateUsuarioDto usuarioDto)
        {
            // 1. Regra de Negócio (Validação)
            if (string.IsNullOrWhiteSpace(usuarioDto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            // 2. Busca a entidade (usando o método novo, sem AsNoTracking)
            var usuarioParaAtualizar = await _usuarioRepository.GetByIdParaAtualizacaoAsync(usuarioDto.Id);

            // 3. Mapeamento (Atualiza o objeto que está "vigiado")
            usuarioParaAtualizar.Nome = usuarioDto.Nome;

            // 4. Salvar
            // (Não precisamos chamar _usuarioRepo.UpdateAsync() porque
            // o DbContext já "viu" a mudança em usuarioParaAtualizar.Nome)
            await _context.SaveChangesAsync();
        }
    }
}
