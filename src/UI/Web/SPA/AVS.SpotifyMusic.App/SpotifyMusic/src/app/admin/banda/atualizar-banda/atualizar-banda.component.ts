import { Guid } from 'guid-typescript/dist/guid';
import { Component, Inject, OnInit } from '@angular/core';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BandaDetalheResponse, BandaRequest, BandaResponse } from '../../../models/banda';
import { BandaService } from '../../../services/banda/banda.service';
import { UploadService } from '../../../services/upload/upload.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-atualizar-banda',
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
  templateUrl: './atualizar-banda.component.html',
  styleUrls: ['./atualizar-banda.component.css']
})
export class AtualizarBandaComponent implements OnInit {
[x: string]: any;

  //inputdata: any;
  bandaResquest: BandaRequest;
  bandaResponse: BandaResponse;
  closemessage = 'closed using directive'
  currentFile?: File;
  progress = 0;
  message = '';
  fotoURL?: string;

  fileName = 'Selecione o arquivo';
  fileInfos?: Observable<any>;
  bandaId: Guid = Guid.createEmpty();

  bandaform = this.buildr.group({
    id: new FormControl(),
    nome: new FormControl(),
    descricao: new FormControl(),
    foto: new FormControl()
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private ref: MatDialogRef<AtualizarBandaComponent>,
              private buildr: FormBuilder,
              private bandaService: BandaService,
              private uploadService: UploadService,
              private toastr: ToastrService,
              private activeRouter: ActivatedRoute,
              private router: Router)
              {
                this.bandaResquest = new BandaRequest();
                this.bandaResponse = new BandaResponse();
              }

  ngOnInit(): void {

    this.CarregarBanda();

  }

  public CarregarBanda(): void {

    this.bandaResquest.id = this.data.id;
    //console.log(this.bandaResquest.id);
    //console.log(Guid.parse(Guid.EMPTY));
    this.fotoURL = this.MostraImagem(this.data.foto);

    if(!(this.bandaResquest.id ===  Guid.EMPTY)){

      //this.spinner.show();
      this.bandaService
        .GetBandaPorId(Guid.parse(this.bandaResquest.id))
        .subscribe((response: BandaResponse) =>
        {
          this.bandaResponse = response;
          if (this.bandaResponse.foto !== '') {
            this.fotoURL = this.MostraImagem(this.bandaResponse.foto);
          }

          this.bandaform.setValue(
            {
              id: this.bandaResponse.id,
              nome: this.bandaResponse.nome,
              descricao: this.bandaResponse.descricao,
              foto: this.bandaResponse.foto
            })
        },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar banda.', 'Erro!');
          console.error(error);
        }
      );//.add(() => this.spinner.hide());
    }

  }

  public CriarBanda(): void {

    const bandaform = this.bandaform.value;
    //this.bandaResquest.id = bandaform.id;
    this.bandaResquest.nome = bandaform.nome;
    this.bandaResquest.descricao = bandaform.descricao;
    this.bandaResquest.foto = bandaform.foto;
    console.log(this.bandaResquest)

    //this.spinner.show();
    this.bandaService
      .Criar(this.bandaResquest)
      .subscribe((response: any) =>
      {
        console.log(response)
        this.toastr.success('Banda criada com sucesso.', 'Sucesso!');
        this.ClosePopup();
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar criar banda.', 'Erro!');
        console.error(error);
      }
    );//.add(() => this.spinner.hide());

  }

  public AtualizarBanda(): void {

    const bandaform = this.bandaform.value;
    this.bandaResquest.id = bandaform.id;
    this.bandaResquest.nome = bandaform.nome;
    this.bandaResquest.descricao = bandaform.descricao;
    this.bandaResquest.foto = bandaform.foto;
    console.log(this.bandaResquest)

    //this.spinner.show();
    this.bandaService
      .Atualizar(this.bandaResquest)
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
      this.bandaResquest.foto = this.fileName;

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
        .UploadImageBanda(Guid.parse(this.bandaResquest.id), this.currentFile)
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

  public ClosePopup(): void {
    this.ref.close('Fechando o popup.');
  }

}
