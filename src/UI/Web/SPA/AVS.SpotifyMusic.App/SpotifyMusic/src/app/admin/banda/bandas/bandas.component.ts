import { BandaService } from './../../../services/banda/banda.service';
import { Component, OnInit, ViewChild  } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource } from '@angular/material/table';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BandaRequest, BandaResponse } from '../../../models/banda';
import { AtualizarBandaComponent } from '../atualizar-banda/atualizar-banda.component';
import { BandaDetalheComponent } from '../banda-detalhe/banda-detalhe.component';
import { RemoveBandaComponent } from '../remove-banda/remove-banda.component';

@Component({
  selector: 'app-bandas',
  standalone: true,
  imports: [
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatPaginator,
    MatPaginatorModule,
    MatTableModule,
    MatIconModule,
    MatInputModule
  ],
  templateUrl: './bandas.component.html',
  styleUrls: ['./bandas.component.css']
})
export class BandasComponent implements OnInit {

  bandas: BandaResponse[] = [];
  dataSource: any;
  displayedColumns: string[] = ["foto", "id", "nome", "descricao", "operacao"];
  @ViewChild(MatPaginator) paginatior !: MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;

  constructor(
    private bandaService: BandaService,
    private dialog: MatDialog
  )
  {
    //this.getBandas();
  }

  ngOnInit() {
    this.GetBandas();
  }

  public GetBandas(): void {
    this.bandaService.GetBandas().subscribe(response =>
      {
        this.bandas = response;
        this.dataSource = new MatTableDataSource<BandaResponse>(this.bandas);
        this.dataSource.paginator = this.paginatior;
        this.dataSource.sort = this.sort;
      });
  }

  public MostraImagem(fotoURL: string): string {
    return fotoURL !== ''
      ? `https://localhost:7170/resources/images/banda/${fotoURL}`
      : '../../../../assets/img/semImagem.jpeg';
  }

  Filterchange(data: Event) {
    const value = (data.target as HTMLInputElement).value;
    this.dataSource.filter = value;
  }

  AtualizarBanda(banda: any) {
    console.log(banda)
    this.OpenPopupAtualizar(banda, 'Atualizar', AtualizarBandaComponent);
  }

  VisualizarBanda(banda: any) {
    console.log(banda)
    this.OpenPopupVisualizar(banda, 'Visualizar', BandaDetalheComponent);
  }

  AdicionarBanda(){
    this.OpenPopupAtualizar(new BandaRequest(), 'Adicionar', AtualizarBandaComponent);
  }

  RemoverBanda(banda: any) {
    console.log(banda)
    this.OpenPopupVisualizar(banda, 'Remover', RemoveBandaComponent);
  }


  OpenPopupVisualizar(banda: any, title: any,component:any) {
    var _popup = this.dialog.open(component, {
      width: '50%',
      enterAnimationDuration: '1000ms',
      exitAnimationDuration: '1000ms',
      data: {
        title: title,
        id: banda.id,
        foto: banda.foto,
        nome: banda.nome,
        descricao: banda.descricao
      }
    });
    _popup.afterClosed().subscribe(item => {
      console.log(item)
      this.GetBandas();
    })
  }

  OpenPopupAtualizar(banda: any, title: any,component:any) {
    var _popup = this.dialog.open(component, {
      width: '25%',
      enterAnimationDuration: '1000ms',
      exitAnimationDuration: '1000ms',
      data: {
        title: title,
        id: banda.id,
        foto: banda.foto,
        nome: banda.nome,
        descricao: banda.descricao
      }
    });
    _popup.afterClosed().subscribe(item => {
      console.log(item)
      this.GetBandas();
    })
  }

}
