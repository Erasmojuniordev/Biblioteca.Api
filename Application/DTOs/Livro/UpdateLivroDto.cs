using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Livro
{
    public class UpdateLivroDto
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public List<int> AutorIds { get; set; } = new List<int>();
    }
}
