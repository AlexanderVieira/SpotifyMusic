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
import { NewGUID } from '../../../utils/NewGUID';


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

  //inputdata: any;
  bandaResquest: BandaRequest;
  bandaResponse: BandaResponse;
  closemessage = 'closed using directive'
  currentFile?: File;
  progress = 0;
  message = '';

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
              private activeRouter: ActivatedRoute,
              private router: Router)
              {
                this.bandaResquest = new BandaRequest();
                this.bandaResponse = new BandaResponse();
              }

  ngOnInit(): void {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];
      //console.log(this.bandaId);
    });

    //this.bandaResquest = this.data;
    //console.log(this.bandaResquest)
    this.bandaResquest.id = this.data.id;
    console.log(this.bandaResquest.id);

    if(!Guid.parse(this.bandaResquest.id).isEmpty()){
      this.SetPopupData(this.bandaResquest.id);
    }
  }

  SetPopupData(id: string) {

    this.bandaService.GetBandaPorId(id).subscribe(response => {
      this.bandaResponse = response;
      console.log(this.bandaResponse)
      this.bandaform.setValue(
        {
          id: this.bandaResponse.id,
          nome: this.bandaResponse.nome,
          descricao: this.bandaResponse.descricao,
          foto: this.bandaResponse.foto
        })
    });
  }

  CriarBanda() {

    const bandaform = this.bandaform.value;
    this.bandaResquest.nome = bandaform.nome;
    this.bandaResquest.descricao = bandaform.descricao;
    this.bandaResquest.foto = bandaform.foto;
    console.log(this.bandaResquest)

    this.bandaService.Criar(this.bandaResquest)
    .subscribe(response =>
    {
      console.log(response)
      this.ClosePopup();
    });
  }

  AtualizarBanda() {

    const bandaform = this.bandaform.value;
    //const bandaRequest = new BandaRequest();
    this.bandaResquest.id = bandaform.id;
    this.bandaResquest.nome = bandaform.nome;
    this.bandaResquest.descricao = bandaform.descricao;
    this.bandaResquest.foto = bandaform.foto;
    console.log(this.bandaResquest)

    this.bandaService.Atualizar(this.bandaResquest)
    .subscribe(response =>
    {
      console.log(response)
      this.ClosePopup();
    });
  }

  SelectFile(event: any): void {
    this.progress = 0;
    this.message = "";

    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      this.currentFile = file;
      this.fileName = this.currentFile.name;
    } else {
      this.fileName = 'Selecione o arquivo';
    }
  }

  Upload(): void {
    if (this.currentFile) {
      this.uploadService.upload(this.currentFile).subscribe({
        next: (event: any) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.progress = Math.round(100 * event.loaded / event.total);
          } else if (event instanceof HttpResponse) {
            this.message = event.body.message;
            this.fileInfos = this.uploadService.getFiles();
          }
        },
        error: (err: any) => {
          console.log(err);
          this.progress = 0;

          if (err.error && err.error.message) {
            this.message = err.error.message;
          } else {
            this.message = 'Could not upload the file!';
          }
        },
        complete: () => {
          this.currentFile = undefined;
        }
      });
    }
  }

  ClosePopup() {
    this.ref.close('Fechando a popup.');
  }

}
