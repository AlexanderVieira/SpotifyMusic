import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BandaDetalheComponent } from './components/streamings/banda/banda-detalhe/banda-detalhe.component';
import { AlbumComponent } from './components/streamings/album/album.component';
import { PlaylistDetalheComponent } from './components/contas/playlist/playlist-detalhe/playlist-detalhe.component';

export const routes: Routes = [
  {
      path: "",
      component: HomeComponent
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
    path: 'playlist-detalhe/:id',
    component: PlaylistDetalheComponent
},

];
