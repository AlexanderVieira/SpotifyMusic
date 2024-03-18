import { BandaService } from './../../services/banda/banda.service';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { BandaResponse } from '../../models/banda';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  bandas: BandaResponse[] = [];

  constructor(private bandaService: BandaService, private router: Router){}

  ngOnInit(): void {
    this.bandaService.getBandas().subscribe(response => {
      console.log(response);
      this.bandas = response;
    })
  }

  goToDetalhe(banda: BandaResponse)
  {
    this.router.navigate(["banda-detalhe", banda.id])
  }

}
