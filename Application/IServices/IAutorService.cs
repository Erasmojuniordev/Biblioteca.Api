using Application.DTOs.Autor;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> GetAllAsync();
        Task<Autor> GetByIdAsync(int id);
        Task AddAsync(CreateAutorDto autor);
        Task UpdateAsync(UpdateAutorDto autor);
        Task DeleteAsync(int id);
    }
}
