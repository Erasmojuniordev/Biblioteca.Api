using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime? DataDevolvidoEm { get; set; }
        public bool Ativo { get; set; }

        // Foreign Keys
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }

        // Navigation Properties
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
    }
}
