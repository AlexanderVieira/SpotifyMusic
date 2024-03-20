import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BandaDetalheComponent } from './components/streamings/banda/banda-detalhe/banda-detalhe.component';
import { AlbumComponent } from './components/streamings/album/album.component';
import { PlaylistDetalheComponent } from './components/contas/playlist/playlist-detalhe/playlist-detalhe.component';
import { PlaylistComponent } from './components/contas/playlist/playlist.component';
import { BandaComponent } from './components/streamings/banda/banda.component';

export const routes: Routes = [
  {
      path: "",
      component: HomeComponent
  },
  {
    path: 'bandas',
    component: BandaComponent
  },
  {
      path: 'banda-detalhe/:id',
      component: BandaDetalheComponent
  },
  {
    path: 'banda/:bandaId/album/:albumId',
    component: AlbumComponent
  },
  {
    path: 'playlists',
    component: PlaylistComponent
  },
  {
    path: 'playlist-detalhe/:id',
    component: PlaylistDetalheComponent
  },

];
