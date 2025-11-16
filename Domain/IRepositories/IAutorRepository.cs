using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAllAsync();
        Task<Autor> GetByIdAsync(int id);
        Task<Autor> GetByIdParaAtualizacaoAsync(int id);

        Task AddAsync(Autor autor);
        Task UpdateAsync(Autor autor);
        Task DeleteAsync(int id);
    }
}
