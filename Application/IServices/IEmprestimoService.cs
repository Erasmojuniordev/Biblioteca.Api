using Application.DTOs.Emprestimo;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IEmprestimoService
    {
        Task<IEnumerable<Emprestimo>> GetAllAsync();
        Task<Emprestimo> GetByIdAsync(int id);
        Task AddAsync(CreateEmprestimoDto emprestimo);
        Task DeleteAsync(int id);
        Task DevolverLivroAsync(int id);
    }
}
