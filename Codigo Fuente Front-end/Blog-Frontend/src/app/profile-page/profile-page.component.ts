import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../../src/_services/user.service';
import { ArticleService } from '../../_services/article.service';
import { User } from '../../../src/_type/user';
import { Article } from '../../_type/article';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponent implements OnInit {
  user: User | null = null;
  userId: string | null = null;
  token: string | null = null;
  editable: boolean = false;
  showPassword: boolean = false;
  errorMessage = '';
  successMessage = '';
  publicArticles: Article[] = [];
  privateArticles: Article[] = [];
  isBlogger: boolean | undefined = false;
  isCurrentUser = false;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private articleService: ArticleService
  ) {}

  ngOnInit() {
    this.successMessage = '';
    this.token = localStorage.getItem('token');
    this.userId = this.route.snapshot.paramMap.get('id');
    const loggedInUserId = localStorage.getItem('userId');

    this.isCurrentUser = this.userId === loggedInUserId;
    this.userService.getUser(this.userId, this.token).subscribe((user: any) => {
      this.user = user;
      if (this.isCurrentUser) {
        this.getAllArticles();
      } else {
        this.getPublicArticles();
      }

      this.isBlogger = this.user?.roles.some((role: any) => role.role === 0);
    });
  }

  getPublicArticles() {
    this.articleService.getPublicArticles(this.token).subscribe(
      (articles: any) => {
        this.publicArticles = articles.filter(
          (article: Article) => article.isPublic
        );
      },
      (error) => {
        this.errorMessage = error;
      }
    );
  }

  getAllArticles() {
    this.articleService
      .getUserArticles(this.token, this.user?.username)
      .subscribe(
        (articles: any) => {
          this.publicArticles = articles.filter(
            (article: Article) => article.isPublic
          );
          this.privateArticles = articles.filter(
            (article: Article) => !article.isPublic
          );
        },
        (error) => {
          this.errorMessage = error;
        }
      );
  }

  onEdit() {
    this.editable = true;
  }

  onSave() {
    this.userService.editProfile(this.user, this.token).subscribe(
      (updatedUser: any) => {
        this.user = updatedUser;
      },
      (error: any) => {
        this.errorMessage = error.error.errorMessage;
      },
      () => {
        this.successMessage = 'Profile updated successfully';
      }
    );

    this.editable = false;
  }

  onChangePassword() {
    this.showPassword = !this.showPassword;
  }

  onCancelChangePassword() {
    this.showPassword = false;
  }

  onCancelEdit() {
    this.editable = false;
  }
}
