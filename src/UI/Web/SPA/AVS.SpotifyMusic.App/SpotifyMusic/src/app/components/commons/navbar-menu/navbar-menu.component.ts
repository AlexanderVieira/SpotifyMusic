import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';
import { PlaylistComponent } from "../../contas/playlist/playlist.component";
import { SidebarComponent } from "../sidebar/sidebar.component";


@Component({
    selector: 'app-navbar-menu',
    standalone: true,
    templateUrl: './navbar-menu.component.html',
    styleUrl: './navbar-menu.component.css',
    imports: [
        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        MatMenuModule,
        MatSidenavModule,
        MatListModule,
        MatExpansionModule,
        MatTooltipModule,
        PlaylistComponent,
        SidebarComponent
    ]
})
export class NavbarMenuComponent {

}
