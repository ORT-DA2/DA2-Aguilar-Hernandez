import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';
import { Observable } from 'rxjs';
import { ArticleService } from '../../_services/article.service';
import { Article } from '../../_type/article';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  user: any;
  isLoggedIn = false;
  isAdmin: boolean | undefined = false;
  isBlogger: boolean | undefined = false;
  notifications: any = [];
  hasNotifications = false;
  searchTerm = '';
  token: string | null = null;
  searchResults: Article[] = [];
  searchError = '';

  constructor(
    private authService: AuthenticationService,
    private articleService: ArticleService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.authService.user$.subscribe((user: any) => {
      this.user = user;
      this.isAdmin = this.user?.roles.some((role: any) => role.role === 1);
      this.isBlogger = this.user?.roles.some((role: any) => role.role === 0);
    });
    this.authService.authStateChanged.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
    });
    this.checkNotifications();
  }

  checkNotifications() {
    const notificationsString = localStorage.getItem('notification');
    if (notificationsString) {
      const notificationsArray = JSON.parse(notificationsString);
      console.log(notificationsArray);
      this.notifications = Array.isArray(notificationsArray)
        ? notificationsArray
        : [];
    } else {
      this.notifications = [];
    }
    this.hasNotifications = this.notifications.length > 0;
  }

  public onSearch(event: Event): void {
    event.preventDefault();

    this.token = localStorage.getItem('token');

    if (this.searchTerm.length > 0) {
      this.articleService.searchArticles(this.searchTerm, this.token).subscribe(
        (results: Article[]) => {
          this.searchResults = results;
        },
        (error: any) => {
          this.searchError = error.error.errorMessage;
        }
      );
    } else {
      this.searchResults = [];
    }
  }

  onSubmitSearch(event: Event) {
    event.preventDefault();

    this.onSearch(event as KeyboardEvent);
  }

  onViewProfile() {
    this.router.navigate(['/profile', this.user?.id]);
  }

  onViewOffensive() {
    this.router.navigate(['/offensive-ranking']);
  }

  onViewActivity() {
    this.router.navigate(['/activity-ranking']);
  }

  onLogin() {
    this.router.navigate(['/login']);
  }

  onRegister() {
    this.router.navigate(['/register']);
  }

  onLogout() {
    this.authService.logout();
    window.location.reload();
    this.router.navigate(['/']);
  }

  onCreateArticle() {
    this.router.navigate(['/create-article']);
  }

  onViewUserManagement() {
    this.router.navigate(['/user-management']);
  }

  onModerateArticle() {
    this.router.navigate(['/articles-management']);
  }

  onImportArticle() {
    this.router.navigate(['/importers']);
  }
}
