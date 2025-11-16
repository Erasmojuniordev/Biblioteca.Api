using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Emprestimo
{
    public class CreateEmprestimoDto
    {
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }
    }
}
