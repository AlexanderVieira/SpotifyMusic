import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { Guid } from 'guid-typescript/dist/guid';
import { Component, OnInit } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { UsuarioService } from '../../../services/usuario/usuario.service';
import { Playlist } from '../../../models/playlist';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { UserResponseLogin } from '../../../models/identity/userResponseLogin';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-playlist',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule, RouterModule],
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css']
})
export class PlaylistComponent implements OnInit {

  //usuarioId = Guid.parse("353f1295-53d0-4b55-bbb5-8aa3c4f789ba");
  usuario!: UserResponseLogin;
  usuarioId = Guid.createEmpty();
  playlists: Playlist[] = [];

  constructor(
    private usuarioService:UsuarioService,
    private authService: AuthService
  ) { }

  ngOnInit() {

    this.usuario = this.authService.getCurrentUserv2();

    if((this.usuario.userToken.id != '') && (this.usuario.userToken.id != undefined)){
      console.log(this.usuario.userToken.id)
      this.usuarioId = Guid.parse(this.usuario.userToken.id);
      console.log(this.usuarioId)
    }

    this.usuarioService.GetPlaylistsAssociadas(this.usuarioId).subscribe(response => {

      console.log(response);
      this.playlists = response;

    });

  }

}
