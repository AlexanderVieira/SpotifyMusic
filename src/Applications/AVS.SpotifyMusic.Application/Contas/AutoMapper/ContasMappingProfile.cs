using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
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

			CreateMap<Usuario, UsuarioRequest>()
				.ForPath(x => x.Senha, m => m.MapFrom(p => p.Senha.Valor))
				.ForPath(x => x.Cpf, m => m.MapFrom(p => p.Cpf.Numero))
				.ForPath(x => x.Email, m => m.MapFrom(p => p.Email.Address));           

            CreateMap<UsuarioDto, Usuario>()
                .ConstructUsing(u =>
                new Usuario(u.Nome, u.Email, u.Cpf, u.Senha, u.Ativo, u.DtNascimento, u.Foto))
                .ForPath(x => x.Senha.Valor, m => m.MapFrom(p => p.Senha))
                .ForPath(x => x.Cpf.Numero, m => m.MapFrom(p => p.Cpf))
                .ForPath(x => x.Email.Address, m => m.MapFrom(p => p.Email));

            CreateMap<Usuario, UsuarioDto>()
                .ForPath(x => x.Senha, m => m.MapFrom(p => p.Senha.Valor))
                .ForPath(x => x.Cpf, m => m.MapFrom(p => p.Cpf.Numero))
                .ForPath(x => x.Email, m => m.MapFrom(p => p.Email.Address));
            //.AfterMap((s, d) =>
            //{
            //	var senha = s.Senha;
            //	var plano = s.Assinaturas.FirstOrDefault(a => a.Ativo)?.Plano;
            //	if(plano != null) 
            //		d.PlanoId = plano.Id;

            //	d.Senha = senha.Valor;

            //});
        }
    }
}
