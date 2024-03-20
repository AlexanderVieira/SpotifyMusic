import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import { BandaResponse } from '../../../models/banda';
import { BandaService } from '../../../services/banda/banda.service';

@Component({
  selector: 'app-banda',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule, RouterModule, HttpClientModule],
  templateUrl: './banda.component.html',
  styleUrl: './banda.component.css'
})
export class BandaComponent {

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
