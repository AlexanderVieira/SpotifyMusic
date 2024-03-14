using AutoMapper;
using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Enums;

namespace AVS.SpotifyMusic.Application.Streamings.AutoMapper
{
    public class StreamingsMappingProfile : Profile
	{
        public StreamingsMappingProfile()
        {
			CreateMap<PlanoRequest, Plano>()
				.ConstructUsing(u =>
				new Plano(u.Nome, u.Descricao, u.Valor, (TipoPlano)u.TipoPlano))
					.ForPath(x => x.TipoPlano, m => m.MapFrom(p => p.TipoPlano));

			CreateMap<Plano, PlanoRequest>()
				.ForPath(x => x.TipoPlano, m => m.MapFrom(p => p.TipoPlano));

            CreateMap<PlanoDto, Plano>()
                .ConstructUsing(u =>
                new Plano(u.Nome, u.Descricao, u.Valor, (TipoPlano)u.TipoPlano))
                    .ForPath(x => x.TipoPlano, m => m.MapFrom(p => p.TipoPlano));

            CreateMap<Plano, PlanoDto>()
                .ForPath(x => x.TipoPlano, m => m.MapFrom(p => p.TipoPlano));
        }
    }
}
