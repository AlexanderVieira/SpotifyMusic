import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PlaylistComponent } from "../../contas/playlist/playlist.component";

@Component({
    selector: 'app-sidebar',
    standalone: true,
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],
    imports: [RouterModule, PlaylistComponent]
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
