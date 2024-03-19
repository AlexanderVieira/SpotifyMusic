import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BandaDetalheResponse } from '../../../models/banda';
import { BandaService } from '../../../services/banda/banda.service';
import { Guid } from 'guid-typescript/dist/guid';
import { Album } from '../../../models/album';

@Component({
  selector: 'app-album',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule],
  templateUrl: './album.component.html',
  styleUrl: './album.component.css'
})
export class AlbumComponent implements OnInit {

  //banda: BandaDetalheResponse;
  albumId: Guid = Guid.createEmpty();
  bandaId: Guid = Guid.createEmpty();
  album = new Album()

  constructor(private bandaService: BandaService, private router: ActivatedRoute){
    //this.banda = new BandaDetalheResponse();
  }

  ngOnInit() {

    this.router.params.subscribe(params => {
      this.albumId = params['albumId'];
      this.bandaId = params['bandaId'];
      console.log(this.albumId);
      console.log(this.bandaId);
    });

    this.bandaService.getAlbumDetalhe(this.bandaId, this.albumId).subscribe(response => {
      console.log(response);
      this.album = response;
    });

  }

}
