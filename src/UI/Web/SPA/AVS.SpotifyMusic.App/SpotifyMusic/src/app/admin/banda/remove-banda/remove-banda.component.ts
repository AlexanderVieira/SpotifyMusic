import { Guid } from 'guid-typescript/dist/guid';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BandaService } from '../../../services/banda/banda.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BandaRequest, BandaResponse } from '../../../models/banda';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-remove-banda',
  standalone: true,
  imports: [
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatToolbarModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './remove-banda.component.html',
  styleUrls: ['./remove-banda.component.css']
})
export class RemoveBandaComponent implements OnInit {

  bandaRequest: BandaRequest;
  bandaResponse: BandaResponse
  bandaId: Guid = Guid.createEmpty();
  fotoURL?: string;

  bandaform = this.buildr.group({
    id: new FormControl(),
    nome: new FormControl(),
    descricao: new FormControl(),
    foto: new FormControl()
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private ref: MatDialogRef<RemoveBandaComponent>,
              private buildr: FormBuilder,
              private bandaService: BandaService,
              private activeRouter: ActivatedRoute)
              {
                this.bandaRequest = new BandaRequest();
                this.bandaResponse = new BandaResponse();
              }

  ngOnInit(): void {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];

    });

    this.bandaResponse = this.data;
    console.log(this.bandaResponse.id)

    this.fotoURL = this.MostraImagem(this.data.foto);

    if (this.bandaResponse.id != Guid.createEmpty())
    {
      this.CarregarBanda(this.bandaResponse.id);
    }
  }

  public CarregarBanda(id: Guid): void {

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

  RemoverBanda() {

    const bandaform = this.bandaform.value;
    this.bandaRequest.id = bandaform.id;
    //const id = Guid.parse(this.bandaRequest.id);
    console.log(this.bandaResponse.id)
    //console.log(id)

    this.bandaService.Remover(Guid.parse(this.bandaRequest.id))
    .subscribe(response =>
    {
      console.log(response)
      this.ClosePopup();
    },error => {

    });
  }

  public MostraImagem(fotoURL?: string): string {
    return fotoURL != '' && fotoURL != undefined
      ? `https://localhost:7170/resources/images/banda/${fotoURL}`
      : '../../../assets/img/semImagem.jpeg';
  }

  ClosePopup(){
    this.ref.close('closing from detail');
  }


}
