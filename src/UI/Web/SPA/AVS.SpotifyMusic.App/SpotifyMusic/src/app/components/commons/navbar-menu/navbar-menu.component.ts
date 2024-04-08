import { Component, OnInit } from '@angular/core';
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
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { CommonModule } from '@angular/common';


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
        CommonModule,
        RouterModule,
        PlaylistComponent,
        SidebarComponent
    ]
})
export class NavbarMenuComponent implements OnInit {

  isCollapsed = true;

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  logout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

  showMenu(): boolean {
    return this.router.url !== '/login';
  }

}
