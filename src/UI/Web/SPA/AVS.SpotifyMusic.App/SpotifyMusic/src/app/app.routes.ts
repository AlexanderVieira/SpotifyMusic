import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BandaDetalheComponent } from './components/streamings/banda/banda-detalhe/banda-detalhe.component';

export const routes: Routes = [
  {
      path: "",
      component: HomeComponent
  },
  {
      path: 'banda-detalhe/:id',
       component: BandaDetalheComponent
  }
];
