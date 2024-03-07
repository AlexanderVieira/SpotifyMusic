using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Contas.AutoMapper
{
	public class ContaMappingProfile : Profile
	{
        public ContaMappingProfile()
        {
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}
