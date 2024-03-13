import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule, MatCardModule, MatIconModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  bandas = [
    { id: 1, nome: 'Kristiyan', descricao: 'descricao da banda', backdrop: 'imagem.jpg' },
    { id: 2, nome: 'Emiliyan', descricao: 'descricao da banda', backdrop: 'imagem.jpg' },
    { id: 3, nome: 'Denitsa', descricao: 'descricao da banda', backdrop: 'imagem.jpg' },
    { id: 4, nome: 'Denitsa', descricao: 'descricao da banda', backdrop: 'imagem.jpg' },
    { id: 5, nome: 'Denitsa', descricao: 'descricao da banda', backdrop: 'imagem.jpg' },
  ];

  goToDetail(_t4: any)
  {
    throw new Error('Method not implemented.');
  }

}
