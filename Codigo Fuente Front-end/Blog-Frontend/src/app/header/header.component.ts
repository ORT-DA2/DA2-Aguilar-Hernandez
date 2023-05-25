import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  notifications: any[] = [];
  hasNotifications = false;
  username: string = '';

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.checkNotifications();
    this.authService.authStateChanged.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
      this.username = this.authService.username;
    });
  }

  checkNotifications() {
    this.hasNotifications = this.notifications.length > 0;
  }

  onSearch(event: Event) {
    event.preventDefault();
  }

  onLogin() {
    this.router.navigate(['/login']);
  }

  getUsername(): string {
    return this.authService.username;
  }

  onRegister() {
    this.router.navigate(['/register']);
  }

  onLogout() {
    this.authService.logout();
  }
}
