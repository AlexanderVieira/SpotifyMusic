using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;

namespace AVS.SpotifyMusic.Application.Contas.AutoMapper
{
    public class ContasMappingProfile : Profile
	{
        public ContasMappingProfile()
        {
			CreateMap<UsuarioRequest, Usuario>()
				.ConstructUsing(u => 
				new Usuario(u.Nome, u.Email, u.Cpf, u.Senha, u.Ativo, u.DtNascimento, u.Foto))
				.ForPath(x => x.Senha.Valor, m => m.MapFrom(p => p.Senha))
				.ForPath(x => x.Cpf.Numero, m => m.MapFrom(p => p.Cpf))
				.ForPath(x => x.Email.Address, m => m.MapFrom(p => p.Email));

            CreateMap<Usuario, UsuarioResponse>()
                .ForPath(x => x.Senha, m => m.MapFrom(p => p.Senha.Valor))
                .ForPath(x => x.Cpf, m => m.MapFrom(p => p.Cpf.Numero))
                .ForPath(x => x.Email, m => m.MapFrom(p => p.Email.Address));

            //CreateMap<UsuarioDetalheResponse, Usuario>()
            //    .ConstructUsing(u =>
            //        new Usuario(u.Nome, u.Email, u.Cpf, u.Senha, u.Ativo, u.DtNascimento, u.Foto))
            //    .ForPath(x => x.Senha.Valor, m => m.MapFrom(p => p.Senha))
            //    .ForPath(x => x.Cpf.Numero, m => m.MapFrom(p => p.Cpf))
            //    .ForPath(x => x.Email.Address, m => m.MapFrom(p => p.Email));           

            CreateMap<Usuario, UsuarioDetalheResponse>()
                .ForPath(x => x.Senha, m => m.MapFrom(p => p.Senha.Valor))
                .ForPath(x => x.Cpf, m => m.MapFrom(p => p.Cpf.Numero))
                .ForPath(x => x.Email, m => m.MapFrom(p => p.Email.Address))
                .ForMember(x => x.Cartoes, opt => opt.Ignore())
                .ForMember(x => x.Assinaturas, opt => opt.Ignore())
                .ForMember(x => x.Playlists, opt => opt.Ignore())
                .AfterMap((s, d) =>
                {
                    d.Cartoes = s.Cartoes.Select(x => 
                                    new CartaoResponse 
                                    { 
                                        Id = x.Id,
                                        Ativo = x.Ativo, 
                                        Nome = x.Nome, 
                                        Numero = x.Numero, 
                                        Expiracao = x.Expiracao, 
                                        Cvv = x.Cvv, 
                                        Limite = x.Limite, 
                                        Pagamento = new PagamentoResponse 
                                        { 
                                            Id = x.Pagamento.Id,
                                            Situacao = (int)x.Pagamento.Situacao, 
                                            Valor = x.Pagamento.Valor.Valor,
                                            Transacao = new TransacaoResponse
                                            {
                                                Id = x.Pagamento.Transacao.Id,
                                                Merchant = x.Pagamento.Transacao.Merchant.Nome,
                                                Descricao = x.Pagamento.Transacao.Descricao,
                                                Situacao = (int)x.Pagamento.Transacao.Situacao,
                                                Valor = x.Pagamento.Transacao.Valor.Valor,
                                                PagamentoId = x.Pagamento.Transacao.PagamentoId
                                            }
                                        },
                                        PagamentoId = x.PagamentoId

                                    }).ToList();

                    d.Assinaturas = s.Assinaturas.Select(x => 
                                    new AssinaturaResponse 
                                    { 
                                        Id = x.Id,
                                        Ativo = x.Ativo, 
                                        Plano = new PlanoResponse 
                                        { 
                                            Id = x.Plano.Id,
                                            Nome = x.Plano.Nome, 
                                            Descricao = x.Plano.Descricao, 
                                            TipoPlano = (int)x.Plano.TipoPlano, 
                                            Valor = x.Plano.Valor.Valor,
                                            AssinaturaId = x.Plano.AssinaturaId
                                        }
                                        
                                    }).ToList();

                    d.Playlists = s.Playlists.Select(x => 
                                    new PlaylistResponse 
                                    { 
                                        Id = x.Id,
                                        Titulo = x.Titulo, 
                                        Descricao = x.Descricao, 
                                        Publica = x.Publica, 
                                        Foto = x.Foto,
                                        Musicas = x.Musicas
                                                   .Select(x => 
                                                        new MusicaResponse 
                                                        { 
                                                            Id = x.Id,
                                                            Nome = x.Nome, 
                                                            Duracao = x.Duracao.Valor 
                                                        }).ToList()                                        

                                    }).ToList();

                });
           
        }
    }       

    //internal class ResponseToModelCollectionConverter : ITypeConverter<UsuarioDto, Usuario>
    //{
    //    public Usuario Convert(UsuarioDto source, Usuario destination, ResolutionContext context)
    //    {
    //        var _mapper = context.Mapper;

    //        var cartoes = _mapper.Map<List<Cartao>>(source.Cartoes);
    //        var assinaturas = _mapper.Map<List<Assinatura>>(source.Assinaturas);
    //        var playlists = _mapper.Map<List<Playlist>>(source.Playlists);
           
    //        destination.AtualizarCartoes(cartoes);
    //        destination.AtualizarAssinaturas(assinaturas);
    //         destination.AtualizarPlaylist(playlists);

    //        return destination;
    //    }
       
    //}
    //internal class ResponseToDtoCollectionConverter : ITypeConverter<Usuario, UsuarioDto>
    //{
    //    public UsuarioDto Convert(Usuario source, UsuarioDto destination, ResolutionContext context)
    //    {
    //        var _mapper = context.Mapper;

    //        var cartoes = _mapper.Map<List<CartaoDto>>(source.Cartoes);
    //        var assinaturas = _mapper.Map<List<AssinaturaDto>>(source.Assinaturas);
    //        var playlists = _mapper.Map<List<PlaylistDto>>(source.Playlists);

    //        destination.Cartoes = cartoes;
    //        destination.Assinaturas = assinaturas;
    //        destination.Playlists = playlists;

    //        return destination;
    //    }

    //}
}
