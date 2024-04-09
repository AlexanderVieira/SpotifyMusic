import { Guid } from 'guid-typescript/dist/guid';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BandaService } from '../../services/banda/banda.service';
import { UploadService } from '../../services/upload/upload.service';
import { ToastrService } from 'ngx-toastr';
import { AlbumRequest, AlbumResponse } from '../../models/album';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { MusicaRequest } from '../../models/musica';
import { HttpEventType, HttpResponse } from '@angular/common/http';


@Component({
  selector: 'app-atualizar-album',
  standalone: true,
  imports: [
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatProgressBarModule,
    MatListModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './atualizar-album.component.html',
  styleUrls: ['./atualizar-album.component.css']
})
export class AtualizarAlbumComponent implements OnInit {

  albumRequest: AlbumRequest;
  albumResponse: AlbumResponse;
  currentFile?: File;
  progress = 0;
  message = '';
  fotoURL!: string;

  fileName = 'Selecione o arquivo';
  fileInfos?: Observable<any>;

  albumform = this.fb.group({

    id: new FormControl(),
    titulo: new FormControl(),
    descricao: new FormControl(),
    foto: new FormControl(),
    bandaId: new FormControl(),
    nomeMusica: new FormControl(),
    duracaoMusica: new FormControl()

  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private ref: MatDialogRef<AtualizarAlbumComponent>,
    private fb: FormBuilder,
    private bandaService: BandaService,
    private uploadService: UploadService,
    private toastr: ToastrService,
  ) {
      this.albumRequest = new AlbumRequest();
      this.albumResponse = new AlbumResponse();
    }

  ngOnInit() {

    this.CarregarAlbum();
  }

  public CarregarAlbum(): void {

    this.albumRequest = this.data;
    this.fotoURL = this.MostraImagem(this.data.foto);
    console.log(this.fotoURL);

    if((this.albumRequest.id != '') && (this.albumRequest.id != undefined)){

      //this.spinner.show();
      this.bandaService
        .GetAlbumDetalhe(this.albumRequest.bandaId, Guid.parse(this.albumRequest.id))
        .subscribe((response: AlbumResponse) =>
        {
          this.albumResponse = response;
          if (this.albumResponse.foto !== '' && this.albumResponse.foto !== undefined) {
            this.fotoURL = this.MostraImagem(this.albumResponse.foto);
            console.log(this.fotoURL)
          }

          this.albumform.setValue(
            {
              id: this.albumResponse.id,
              titulo: this.albumResponse.titulo,
              descricao: this.albumResponse.descricao,
              foto: this.albumResponse.foto,
              bandaId: this.albumResponse.bandaId,
              nomeMusica: undefined,
              duracaoMusica: undefined
            })

        },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar banda.', 'Erro!');
          console.error(error);
        }
      );//.add(() => this.spinner.hide());
    }

  }

  public CriarAlbum(): void {

    const albumform = this.albumform.value;
    this.albumRequest.titulo = albumform.titulo;
    this.albumRequest.descricao = albumform.descricao;
    this.albumRequest.foto = albumform.foto;
    this.albumRequest.bandaId = this.data.bandaId

    const musica = new MusicaRequest();
    musica.nome = albumform.nomeMusica;
    musica.duracao = albumform.duracaoMusica;
    this.albumRequest.musicas.push(musica);

    //this.albumRequest.musica = new MusicaRequest();
    //this.albumRequest.musica.nome = albumform.nomeMusica;
    //this.albumRequest.musica.duracao = albumform.duracaoMusica;
    console.log(this.albumRequest)

    //this.spinner.show();
    this.bandaService
      .CriarAlbum(this.albumRequest)
      .subscribe((response: any) =>
      {
        console.log(response)
        this.toastr.success('Album criado com sucesso.', 'Sucesso!');
        this.ClosePopup();
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar criar album.', 'Erro!');
        console.error(error);
      }
    );//.add(() => this.spinner.hide());

  }

  public AtualizarAlbum(): void {

    const albumform = this.albumform.value;
    this.albumRequest.titulo = albumform.titulo;
    this.albumRequest.descricao = albumform.descricao;
    this.albumRequest.foto = albumform.foto;
    this.albumRequest.bandaId = this.data.bandaId

    const musica = new MusicaRequest();
    musica.nome = albumform.nomeMusica;
    musica.duracao = albumform.duracaoMusica;
    this.albumRequest.musicas.push(musica);

    //this.spinner.show();
    this.bandaService
      .AtualizarAlbum(this.albumRequest)
      .subscribe((response: any) =>
      {
        console.log(response)
        this.toastr.success('Banda atualizada com sucesso.', 'Sucesso!');
        this.ClosePopup();
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar atualizar banda.', 'Erro!');
        console.error(error);
      }
    );//.add(() => this.spinner.hide());

  }

  public MostraImagem(fotoURL?: string): string {
    return fotoURL != '' && fotoURL != undefined
      ? `https://localhost:7170/resources/images/banda/${fotoURL}`
      : '../../../assets/img/semImagem.jpeg';
  }

  public OnFileChange(event: any): void {
    this.progress = 0;
    this.message = "";
    console.log(event);

    if (event.target.files && event.target.files[0]) {

      const file: File = event.target.files[0];
      this.currentFile = file;
      this.fileName = this.currentFile.name;
      console.log(this.fileName);
      this.albumRequest.foto = this.fileName;

      const reader = new FileReader();
      reader.onload = (event: any) => this.fotoURL = event.target.result;
      reader.readAsDataURL(file);

      this.UploadImage();

    }
    else {
      this.fileName = 'Selecione o arquivo';
    }
  }

  public UploadImage(): void {

    if (this.currentFile) {
      this.uploadService
        .UploadImageBanda(Guid.parse(this.albumRequest.id), this.currentFile)
        .subscribe({
          next: (event: any) => {
            if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round(100 * event.loaded / event.total);
            } else if (event instanceof HttpResponse) {
              this.message = event.body.message;
              this.fileInfos = this.uploadService.getFiles();
            }

            this.toastr.success('Upload realizado com sucesso.', 'Sucesso!');

          },
          error: (err: any) => {
            console.log(err);
            this.progress = 0;

            if (err.error && err.error.message) {
              this.message = err.error.message;

            } else {
              this.message = 'Erro ao tentar fazer upload do arquivo!';
              this.toastr.error('Erro ao tentar fazer upload do arquivo.', 'Erro!');
            }
          },
          complete: () => {
            this.currentFile = undefined;
          }
        });
    }
  }

  ClosePopup() {
    this.ref.close('Fechando o popup.');
  }

}
