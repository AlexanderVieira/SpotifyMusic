using AutoMapper;
using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;

namespace AVS.SpotifyMusic.Application.Pagamentos.AutoMapper
{
    public class PagamentosMappingProfile : Profile
	{
        public PagamentosMappingProfile()
        {
			CreateMap<PagamentoDto, Pagamento>()
				.ForPath(x => x.Situacao, m => m.MapFrom(p  => p.Situacao));

			CreateMap<Pagamento, PagamentoDto>()
				.ForPath(x => x.Situacao, m => m.MapFrom(p => p.Situacao));

            CreateMap<CartaoRequest, Cartao>()
                .ConstructUsing(u =>
                new Cartao(u.Numero, u.Nome, u.Expiracao, u.Cvv, u.Ativo, u.Limite))
                    .ForPath(x => x.Limite.Valor, m => m.MapFrom(p => p.Limite));

            CreateMap<Cartao, CartaoRequest>()
                .ForPath(x => x.Limite, m => m.MapFrom(p => p.Limite.Valor));

            CreateMap<CartaoDto, Cartao>()
                .ConstructUsing(u =>
                new Cartao(u.Numero, u.Nome, u.Expiracao, u.Cvv, u.Ativo, u.Limite))
                    .ForPath(x => x.Limite.Valor, m => m.MapFrom(p => p.Limite));

            CreateMap<Cartao, CartaoDto>()
                .ForPath(x => x.Limite, m => m.MapFrom(p => p.Limite.Valor));
        }
    }
}
