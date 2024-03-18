import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BandaService } from '../../../../services/banda/banda.service';
import { BandaDetalheResponse } from '../../../../models/banda';
import { Guid } from 'guid-typescript/dist/guid';

@Component({
  selector: 'app-banda-detalhe',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule],
  templateUrl: './banda-detalhe.component.html',
  styleUrls: ['./banda-detalhe.component.css']
})
export class BandaDetalheComponent implements OnInit {

  banda: BandaDetalheResponse;
  bandaId: Guid = Guid.createEmpty();

  constructor(private bandaService: BandaService, private router: ActivatedRoute){
    this.banda = new BandaDetalheResponse();
  }

  ngOnInit() {

    this.router.params.subscribe(params => {
      this.bandaId = params['id'];
      console.log(this.bandaId);
    });

    this.bandaService.getBandaDetalhe(this.bandaId).subscribe(response => {
      console.log(response);
      this.banda = response;
    });
  }

}
