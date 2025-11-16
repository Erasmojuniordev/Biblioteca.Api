using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        // Navigation Properties
        public List<Emprestimo> Emprestimos { get; set; }
        public List<Autor> Autores { get; set; }
    }
}
