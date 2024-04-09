import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { FooterComponent } from "../commons/footer/footer.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [MatButtonModule, MatCardModule, MatIconModule, HttpClientModule, FooterComponent]
})
export class HomeComponent implements OnInit {

  constructor(){}

  ngOnInit(): void {

  }

}
