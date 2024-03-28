import { Guid } from 'guid-typescript/dist/guid';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BandaService } from '../../../services/banda/banda.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BandaDetalheResponse } from '../../../models/banda';

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

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private ref: MatDialogRef<BandaDetalheComponent>,
              private service: BandaService,
              private activeRouter: ActivatedRoute)
              {
                this.bandaDetalheResponse = new BandaDetalheResponse();
              }

  ngOnInit(): void {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];

    });

    this.bandaDetalheResponse = this.data;
    //this.bandaDetalheResponse.id = this.inputdata.id
    console.log(this.bandaDetalheResponse.id)

    if (this.bandaDetalheResponse.id != Guid.createEmpty())
    {
      this.service.GetBandaDetalhe(this.bandaDetalheResponse.id).subscribe(response =>
      {
        this.bandaDetalheResponse = response;
      });
    }
  }

  closepopup(){
    this.ref.close('closing from detail');
  }

}
