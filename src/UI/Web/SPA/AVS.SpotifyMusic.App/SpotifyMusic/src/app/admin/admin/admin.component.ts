import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { BandaComponent } from "../banda/banda.component";
import { BandasComponent } from "../banda/bandas/bandas.component";


@Component({
    selector: 'app-admin',
    standalone: true,
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css'],
    imports: [MatTabsModule, MatIconModule, BandaComponent, BandasComponent]
})
export class AdminComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
