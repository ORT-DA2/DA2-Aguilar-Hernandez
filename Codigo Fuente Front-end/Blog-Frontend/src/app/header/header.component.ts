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
  isAdmin: boolean | undefined = false;
  notifications: any[] = [];
  hasNotifications = false;
  username: string = '';
  roles: string | null = '';

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.checkNotifications();
    this.isLoggedIn = this.authService.isAuthenticated();
    this.authService.authStateChanged.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
      this.username = localStorage.getItem('username') || '';
      this.isAdmin = localStorage.getItem('isAdmin') === 'true';
    });
    this.username = localStorage.getItem('username') || '';
    this.isAdmin = localStorage.getItem('isAdmin') === 'true';
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

  onRegister() {
    this.router.navigate(['/register']);
  }

  onLogout() {
    this.authService.logout();
  }
}
