using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<IEnumerable<Emprestimo>> GetAllAsync();
        Task<Emprestimo> GetByIdAsync(int id);

        Task AddAsync(Emprestimo emprestimo);
        Task DeleteAsync(int id);

        
        Task<IEnumerable<Emprestimo>> GetByUsuarioIdAsync(int usuarioId, bool apenasAtivos);
        Task<IEnumerable<Emprestimo>> GetByLivroIdAsync(int livroId, bool apenasAtivos);
        Task<Emprestimo> GetByIdParaAtualizacaoAsync(int id);

    }
}
