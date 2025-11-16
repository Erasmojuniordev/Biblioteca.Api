using Application.DTOs.Autor;
using Application.DTOs.Emprestimo;
using Application.DTOs.Livro;
using Application.DTOs.Usuario;
using AutoMapper;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            // Diz ao AutoMapper: "Qualquer DTO de criação,
            // pode ser mapeado para a entidade correspondente."
            CreateMap<CreateAutorDto, Autor>();
            CreateMap<CreateLivroDto, Livro>();
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<CreateEmprestimoDto, Emprestimo>();

            // Mapeamentos de DTO de Update -> Entidade
            CreateMap<UpdateAutorDto, Autor>();
            CreateMap<UpdateLivroDto, Livro>();
            CreateMap<UpdateUsuarioDto, Usuario>();
        }
    }
}
