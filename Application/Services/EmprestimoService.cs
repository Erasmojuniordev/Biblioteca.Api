using Application.DTOs.Emprestimo;
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
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepo;
        private readonly ILivroRepository _livroRepo; // Regra de negocio pra verificar disponibilidade
        private readonly BibliotecaDbContext _context;

        public EmprestimoService(IEmprestimoRepository emprestimoRepo, BibliotecaDbContext context, ILivroRepository livroRepo)
        {
            _emprestimoRepo = emprestimoRepo;
            _context = context;
            _livroRepo = livroRepo;
        }

        public async Task<IEnumerable<Emprestimo>> GetAllAsync()
        {
            return await _emprestimoRepo.GetAllAsync();
        }

        public Task<Emprestimo> GetByIdAsync(int id)
        {
            return _emprestimoRepo.GetByIdAsync(id);
        }
        public async Task AddAsync(CreateEmprestimoDto emprestimoDto)
        {
            var dataAtual = DateTime.Now.Date;

            // Regra 1: O usuário está bloqueado?
            var emprestimosAtivosUsuario = await _emprestimoRepo.GetByUsuarioIdAsync(emprestimoDto.UsuarioId, true);

            // .Any() checa se *qualquer* item na lista atende à condição
            var temAtraso = emprestimosAtivosUsuario.Any(e => e.DataDevolucao.Date < dataAtual);

            if (temAtraso)
            {
                throw new Exception("Usuário bloqueado. Existem empréstimos atrasados.");
            }

            // Regra 2: O livro está disponível?
            var emprestimosAtivosLivro = await _emprestimoRepo.GetByLivroIdAsync(emprestimoDto.LivroId, true);

            if (emprestimosAtivosLivro.Any())
            {
                throw new Exception("O livro não está disponível. Já está emprestado.");
            }

            var novoEmprestimo = new Emprestimo
            {
                UsuarioId = emprestimoDto.UsuarioId,
                LivroId = emprestimoDto.LivroId,
                DataEmprestimo = dataAtual,
                DataDevolucao = dataAtual.AddDays(14),
                Ativo = true,
                DataDevolvidoEm = null
            };

            await _emprestimoRepo.AddAsync(novoEmprestimo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _emprestimoRepo.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task DevolverLivroAsync(int emprestimoId)
        {
            var emprestimo = await _emprestimoRepo.GetByIdParaAtualizacaoAsync(emprestimoId);

            // Regra 1: Já foi devolvido?
            if (emprestimo.Ativo == false)
            {
                throw new Exception("Este livro já foi devolvido.");
            }

            emprestimo.Ativo = false;
            emprestimo.DataDevolvidoEm = DateTime.Now;

            await _context.SaveChangesAsync();
        }

    }
}
