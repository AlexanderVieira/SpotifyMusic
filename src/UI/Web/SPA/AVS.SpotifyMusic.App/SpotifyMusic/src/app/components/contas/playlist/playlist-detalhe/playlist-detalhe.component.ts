import { Guid } from 'guid-typescript/dist/guid';
import { HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table';
import { UsuarioService } from './../../../../services/usuario/usuario.service';
import { Playlist } from '../../../../models/playlist';

@Component({
  selector: 'app-playlist-detalhe',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, MatGridListModule, MatTableModule, HttpClientModule, RouterModule],
  templateUrl: './playlist-detalhe.component.html',
  styleUrls: ['./playlist-detalhe.component.css']
})
export class PlaylistDetalheComponent implements OnInit {

  usuarioId = Guid.parse("353f1295-53d0-4b55-bbb5-8aa3c4f789ba");
  playlistId =  Guid.createEmpty();
  playlists: Playlist[] = [];
  playlist: Playlist;

  constructor(private UsuarioService: UsuarioService, private activeRouter: ActivatedRoute, private router: Router)
  {
    this.playlist = new Playlist();
  }

  ngOnInit() {

    this.activeRouter.params.subscribe(params => {
      this.playlistId = params['id'];
      console.log(this.playlistId);
    });

    this.UsuarioService.GetPlaylistsAssociadas(this.usuarioId).subscribe(response =>
      {
        console.log(response);
        this.playlists = response;
        const playlistId = this.playlistId;
        for (let index = 0; index < this.playlists.length; index++) {

          if(this.playlists[index].id == playlistId){
            this.playlist = this.playlists[index];
          }

        }
      });
  }

}
