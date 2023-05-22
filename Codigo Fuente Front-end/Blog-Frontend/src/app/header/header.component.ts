import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  notifications: any[] = [];
  hasNotifications = false;

  ngOnInit() {
    this.checkNotifications();
  }

  checkNotifications() {
    this.hasNotifications = this.notifications.length > 0;
  }

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
