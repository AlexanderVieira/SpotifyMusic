import { UploadService } from './../../../services/upload/upload.service';
import { MusicaRequest } from './../../../models/musica';
import { Guid } from 'guid-typescript/dist/guid';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BandaService } from '../../../services/banda/banda.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BandaDetalheResponse } from '../../../models/banda';
import { ToastrService } from 'ngx-toastr';
import { AtualizarBandaComponent } from '../atualizar-banda/atualizar-banda.component';
import { AlbumRequest } from '../../../models/album';
import { AtualizarAlbumComponent } from '../../atualizar-album/atualizar-album.component';

@Component({
  selector: 'app-banda-detalhe',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, RouterModule],
  templateUrl: './banda-detalhe.component.html',
  styleUrls: ['./banda-detalhe.component.css']
})
export class BandaDetalheComponent implements OnInit {

  bandaDetalheResponse: BandaDetalheResponse;
  //banda: any;
  bandaId: Guid = Guid.createEmpty();
  fotoURL!: string;
  fotoAlbumURL!: string;
  file!: File;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private ref: MatDialogRef<BandaDetalheComponent>,
    private bandaService: BandaService,
    private uploadService: UploadService,
    private activeRouter: ActivatedRoute,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {
    this.bandaDetalheResponse = new BandaDetalheResponse();
  }

  ngOnInit(): void {
    this.CarregarBanda();
  }

  public CarregarBanda(): void {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];
      console.log(this.bandaId)
    });

    this.bandaDetalheResponse = this.data;
    console.log(this.bandaDetalheResponse.id)

    if (this.bandaDetalheResponse.id != Guid.createEmpty())
    {
      //this.spinner.show();
      this.bandaService
        .GetBandaDetalhe(this.bandaDetalheResponse.id)
        .subscribe((response: BandaDetalheResponse) =>
        {
          this.bandaDetalheResponse = response;
          if (this.bandaDetalheResponse.foto !== '') {
            this.fotoURL = 'https://localhost:7170' + '/resources/images/banda/' + this.bandaDetalheResponse.foto;
          }
        },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar banda.', 'Erro!');
          console.error(error);
        }
      );//.add(() => this.spinner.hide());
    }
  }

  public AdicionarAlbum(): void {

    const albumRequest = new AlbumRequest();
    albumRequest.bandaId = this.bandaDetalheResponse.id
    console.log(albumRequest.bandaId);
    this.OpenPopupAtualizar(albumRequest, 'Adicionar', AtualizarAlbumComponent);

  }

  OpenPopupAtualizar(album: any, title: any,component:any) {
    var _popup = this.dialog.open(component, {
      width: '30%',
      enterAnimationDuration: '1000ms',
      exitAnimationDuration: '1000ms',
      data: {
        title: title,
        id: album.id,
        foto: album.foto,
        titulo: album.titulo,
        descricao: album.descricao,
        bandaId: album.bandaId,
        musica: album.MusicaRequest
      }
    });
    _popup.afterClosed().subscribe(item => {
      console.log(item)
      this.CarregarBanda();
    })
  }

  ClosePopup(){
    this.ref.close('Fechando detalhe da banda');
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.fotoAlbumURL = event.target.result;

    this.file = ev.target.files[0];
    reader.readAsDataURL(this.file);

    this.uploadImagem();
  }

  uploadImagem(): void {
    //this.spinner.show();
    this.uploadService.UploadImageBanda(this.bandaDetalheResponse.id, this.file).subscribe(
      () => {

        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
        this.CarregarBanda();
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ); //.add(() => this.spinner.hide());
  }

  public MostraImagem(fotoAlbumURL?: string): string {
    return fotoAlbumURL !== null
      ? `https://localhost:7170/resources/images/banda/${fotoAlbumURL}`
      : '../../../../assets/img/semImagem.jpeg';
  }

}
