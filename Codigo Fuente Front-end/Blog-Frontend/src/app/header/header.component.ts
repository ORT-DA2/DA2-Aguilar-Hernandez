import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  isAdmin: boolean = false;
  notifications: any[] = [];
  hasNotifications = false;
  username: string = '';
  roles: string | null = '';
  subscription: Subscription | undefined;

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
      this.refreshAdminStatus();
    });
    this.username = localStorage.getItem('username') || '';
    this.refreshAdminStatus();
  }

  checkNotifications() {
    this.hasNotifications = this.notifications.length > 0;
  }

  refreshAdminStatus() {
    if (this.isLoggedIn) {
      const authorization = localStorage.getItem('token') || '';
      this.authService.isAdmin(authorization).subscribe((isAdmin: boolean) => {
        this.isAdmin = isAdmin;
      });
    } else {
      this.isAdmin = false;
    }
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
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
