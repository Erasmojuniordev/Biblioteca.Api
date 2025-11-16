using Application.DTOs.Usuario;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDto usuario);
        Task UpdateAsync(UpdateUsuarioDto usuario);
        Task DeleteAsync(int id);
    }
}
