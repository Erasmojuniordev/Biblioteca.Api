using Application.DTOs.Livro;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> GetAllAsync();
        Task<Livro> GetByIdAsync(int id);

        Task AddAsync(CreateLivroDto livro);
        Task UpdateAsync(UpdateLivroDto livro);
        Task DeleteAsync(int id);
    }
}
