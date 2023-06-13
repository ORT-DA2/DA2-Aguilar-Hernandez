import { Component } from '@angular/core';
import { Article } from '../../_type/article';
import { ArticleService } from '../../_services/article.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent {
  imgPath: string = '';
  lastArticles: Article[] = [];
  token: string | null = null;

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.loadLastArticles();
    this.setLoggedUser();
  }

  loadLastArticles(): void {
    this.articleService.getLastArticles().subscribe(
      (articles: any) => {
        this.lastArticles = articles;
      },
      (error: any) => {
        console.error('An error occurred while loading last articles:', error);
      }
    );
  }

  setLoggedUser(): void{
    if(!this.token){
      this.token = localStorage.getItem('token');
    }
  }
}
