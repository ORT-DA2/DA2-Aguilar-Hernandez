import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  user: any;
  isLoggedIn = false;
  isAdmin = false;
  notifications: any[] = [];
  hasNotifications = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.checkNotifications();
    this.isLoggedIn = this.authService.isAuthenticated();
    this.authService.user$.subscribe((user: any) => {
      this.user = user;
    });
    this.authService.authStateChanged.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
    });
    this.refreshAdminStatus();
  }

  checkNotifications() {
    this.hasNotifications = this.notifications.length > 0;
  }

  refreshAdminStatus() {
    if (this.isLoggedIn) {
      const authorization = localStorage.getItem('token') || '';
      this.authService.user$.subscribe((user: any) => {
        this.user = user;
      });
      this.authService.isAdmin(authorization).subscribe((isAdmin: boolean) => {
        this.isAdmin = isAdmin;
      });
    } else {
      this.isAdmin = false;
    }
  }

  onSearch(event: Event) {
    event.preventDefault();
  }

  onViewProfile() {
    this.router.navigate(['/profile', this.user?.id]);
  }

  onLogin() {
    this.router.navigate(['/login']);
  }

  onRegister() {
    this.router.navigate(['/register']);
  }

  onLogout() {
    this.router.navigate(['/']);
    this.authService.logout();
  }
}
