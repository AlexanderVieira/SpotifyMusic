import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarMenuComponent } from "./components/commons/navbar-menu/navbar-menu.component";
import { FooterComponent } from "./components/commons/footer/footer.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, NavbarMenuComponent, FooterComponent]
})
export class AppComponent {
  title = 'SpotifyMusic';
}
