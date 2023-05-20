import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  isLoggedIn = false;
  notifications: any[] = [];

  onSearch(event: Event) {
    event.preventDefault();
  }

  onLogin() {
    this.isLoggedIn = true;
  }

  onRegister() {}

  onLogout() {
    this.isLoggedIn = false;
  }
}
