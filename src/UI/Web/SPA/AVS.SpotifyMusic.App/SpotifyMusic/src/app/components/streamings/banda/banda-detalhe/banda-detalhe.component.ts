import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { BandaService } from '../../../../services/banda/banda.service';
import { BandaDetalheResponse } from '../../../../models/banda';
import { Guid } from 'guid-typescript/dist/guid';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Album } from '../../../../models/album';

@Component({
  selector: 'app-banda-detalhe',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, RouterModule, HttpClientModule],
  templateUrl: './banda-detalhe.component.html',
  styleUrls: ['./banda-detalhe.component.css']
})
export class BandaDetalheComponent implements OnInit {

  banda: BandaDetalheResponse;
  bandaId: Guid = Guid.createEmpty();
  //albuns: Album[] = [];
  //aux: Album[] = [];

  constructor(private bandaService: BandaService, private activeRouter: ActivatedRoute, private router: Router){
    this.banda = new BandaDetalheResponse();
  }

  ngOnInit() {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];
      console.log(this.bandaId);
    });

    this.bandaService.getBandaDetalhe(this.bandaId).subscribe(response => {
      console.log(response);
      this.banda = response;
    });
  }

  goToDetalhe(album: Album)
  {
    this.router.navigate([`banda/${album.bandaId}/album/${album.id}`])
  }

}
