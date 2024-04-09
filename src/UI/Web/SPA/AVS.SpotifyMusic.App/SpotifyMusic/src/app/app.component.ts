import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarMenuComponent } from "./components/commons/navbar-menu/navbar-menu.component";
import { FooterComponent } from "./components/commons/footer/footer.component";
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';
import { PlaylistComponent } from "./components/contas/playlist/playlist.component";
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/commons/sidebar/sidebar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth/auth.service';
import { UserResponseLogin } from './models/identity/userResponseLogin';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [
        RouterOutlet,
        RouterModule,
        NavbarMenuComponent,
        FooterComponent,
        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        MatMenuModule,
        MatSidenavModule,
        MatListModule,
        MatExpansionModule,
        MatTooltipModule,
        PlaylistComponent,
        HomeComponent,
        SidebarComponent,
        FormsModule,
        ReactiveFormsModule,
        CommonModule
    ]
})
export class AppComponent {
  title = 'SpotifyMusic';

  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(): void {
    let user: UserResponseLogin;

    if ((localStorage.getItem('user') != '') && (localStorage.getItem('user') != undefined))
      user = JSON.parse(localStorage.getItem('user') ?? '{}');
    else
      user = new UserResponseLogin();


    if ((user.accessToken != '') && (user.accessToken != undefined))
      this.authService.setCurrentUser(user);
  }
}
