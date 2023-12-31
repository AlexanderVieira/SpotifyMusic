﻿using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Infra.Data.Context;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;

namespace AVS.SpotifyMusic.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository  //<Usuario> where TEntity : Entity, IAggregateRoot
    {
        public UsuarioRepository(SpotifyMusicContext context) : base(context)
        {
        }

        //public void Ativar(Usuario usuario)
        //{            
        //    Query.Update(usuario);
        //}

        //public void Inativar(Usuario usuario)
        //{            
        //    Query.Update(usuario);
        //}
    }
    
}
