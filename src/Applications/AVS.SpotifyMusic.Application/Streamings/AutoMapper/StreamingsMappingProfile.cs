using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
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

            CreateMap<Plano, PlanoResponse>()
                .ForPath(x => x.TipoPlano, m => m.MapFrom(p => p.TipoPlano));

            CreateMap<BandaRequest, Banda>()
                .ConstructUsing(u => new Banda(u.Nome, u.Descricao, u.Foto));                    

            CreateMap<Banda, BandaResponse>();                

            CreateMap<Banda, BandaDetalheResponse>()                
                .ForMember(x => x.Albuns, opt => opt.Ignore())                
                .AfterMap((s, d) =>
                {
                    d.Albuns = s.Albuns.Select(x =>
                                    new AlbumResponse
                                    {
                                        Id = x.Id,
                                        Titulo = x.Titulo,
                                        Descricao = x.Descricao,
                                        Foto = x.Foto,
                                        Musicas = x.Musicas.Select(x => 
                                        new MusicaResponse
                                        {
                                            Id = x.Id,
                                            Nome = x.Nome,
                                            Duracao = x.Duracao.Valor,
                                            Playlists = x.Playlists.Select(p => 
                                            new PlaylistResponse
                                            {
                                                Id = p.Id,
                                                Titulo = p.Titulo,
                                                Descricao = p.Descricao,
                                                Publica = p.Publica,
                                                Foto = p.Foto,
                                                Usuario = new UsuarioResponse
                                                {
                                                    Id = p.Usuario.Id,
                                                    Nome = p.Usuario.Nome,
                                                    Email = p.Usuario.Email.Address
                                                }
                                            }).ToList()

                                        }).ToList()

                                    }).ToList();                   

                });

            CreateMap<AlbumRequest, Album>()
                .ForPath(x => x.Musicas, opt => opt.Ignore())
                .AfterMap((s, d) => 
                {
                    var musicas = s.Musicas.Select(x => new Musica(x.Nome, x.Duracao)).ToList();
                    d.AtualizarMusicas(musicas);
                });

            CreateMap<Album, AlbumResponse>()
                .ForMember(x => x.Musicas, opt => opt.Ignore())                
                .AfterMap((s, d) =>
                {
                    d.Musicas = s.Musicas.Select(x => 
                    new MusicaResponse
                    { 
                        Id = x.Id, 
                        Nome = x.Nome, 
                        Duracao = x.Duracao.Valor, 
                        Playlists = x.Playlists.Select(p => 
                        new PlaylistResponse
                        {
                            Id = p.Id,
                            Titulo = p.Titulo,
                            Descricao = p.Descricao,
                            Foto = p.Foto,
                            Publica = p.Publica
                        }).ToList()

                    }).ToList();

                });

        }
    }
    
}
