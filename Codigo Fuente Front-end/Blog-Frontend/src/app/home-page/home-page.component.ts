import { Component } from '@angular/core';
import { Article } from '../../_type/article';
import { ArticleService } from '../../_services/article.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent {
  imgPath: string = '';
  lastArticles: Article[] = [];
  token: string | null = null;

  constructor(private articleService: ArticleService, private router: Router) {}

  ngOnInit(): void {
    this.loadLastArticles();
    this.setLoggedUser();
  }

  loadLastArticles(): void {
    this.articleService.getLastArticles().subscribe((articles: any) => {
      this.lastArticles = articles;
    });
  }

  setLoggedUser(): void {
    if (!this.token) {
      this.token = localStorage.getItem('token');
    }
  }

  onClick(username: string) {
    this.router.navigate(['/profile', username]);
  }
}
