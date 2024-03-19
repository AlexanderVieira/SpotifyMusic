import { Guid } from 'guid-typescript/dist/guid';
import { Component, OnInit } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { UsuarioService } from '../../../services/usuario/usuario.service';
import { RouterModule } from '@angular/router';
import { Playlist } from '../../../models/playlist';

@Component({
  selector: 'app-playlist',
  standalone: true,
  imports: [
    MatListModule,
    MatExpansionModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css']
})
export class PlaylistComponent implements OnInit {

  usuarioId = Guid.parse("353f1295-53d0-4b55-bbb5-8aa3c4f789ba");
  playlists: Playlist[] = [];

  constructor(private usuarioService:UsuarioService) { }

  ngOnInit() {

    this.usuarioService.getPlaylistsDoUsuario(this.usuarioId).subscribe(response => {

      console.log(response);
      this.playlists = response;

    });

  }

}