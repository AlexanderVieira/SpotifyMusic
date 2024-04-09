import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { BandaDetalheResponse } from '../../../models/banda';
import { BandaService } from '../../../services/banda/banda.service';
import { Guid } from 'guid-typescript/dist/guid';
import { Album } from '../../../models/album';
import { Musica } from '../../../models/musica';
import { UsuarioService } from '../../../services/usuario/usuario.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-album',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule, RouterModule],
  templateUrl: './album.component.html',
  styleUrl: './album.component.css'
})
export class AlbumComponent implements OnInit {


  albumId: Guid = Guid.createEmpty();
  bandaId: Guid = Guid.createEmpty();
  album = new Album();
  musica = new Musica()

  constructor(
    private bandaService: BandaService,
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    private router: ActivatedRoute
  ){}

  ngOnInit() {

    this.router.params.subscribe(params => {
      this.albumId = params['albumId'];
      this.bandaId = params['bandaId'];
      console.log(this.albumId);
      console.log(this.bandaId);
    });

    this.bandaService
      .GetAlbumDetalhe(this.bandaId, this.albumId)
      .subscribe(response => {
        console.log(response);
        this.album = response;
    });

  }

  public AdicionarMusicaPlaylist(musica: Musica) {
    this.usuarioService
      .AdicionarMusicaPlaylist(this.bandaId, musica.id)
      .subscribe((response: any) => {

        this.ResponseHandler(response);

      },
        (error: any) => {

          this.ResponseHandler(error);

      })
  }

  public MostraImagem(fotoURL?: string): string {
    return fotoURL != '' && fotoURL != undefined
      ? `https://localhost:7170/resources/images/banda/${fotoURL}`
      : '../../../assets/img/semImagem.jpeg';
  }

  private ResponseHandler(error: HttpErrorResponse): void {
    switch (error.status) {
      case 200:
        this.toastr.success('Música adicionada na playlist com sucesso.', 'Sucesso!');
        break;
      case 400:
        this.toastr.error('Erro ao tentar adicionar música.', 'Erro!');
        break;
      case 401:
        this.toastr.error('Usuário não autenticado.', 'Erro!');
        break;
      default:
        this.toastr.error('Ocorreu um erro genérico.', 'Erro!');
        break;
    }
  }

}
