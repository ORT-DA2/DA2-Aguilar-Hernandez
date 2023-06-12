import { Component } from '@angular/core';
import {ArticleService} from "../../_services/article.service";
import {Article} from "../../_type/article";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-article-profile',
  templateUrl: './article-profile.component.html',
  styleUrls: ['./article-profile.component.css']
})
export class ArticleProfileComponent {

  id!: string;
  article: Article = {} as Article;
  token: string | null = null;

  constructor(private articleService: ArticleService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void{
    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.token = localStorage.getItem('token');
    this.loadArticle();
  }

  loadArticle(){
    this.articleService.getArticleById(this.id, this.token).subscribe(
      (article: Article)=>{
        this.article = article;
      },
      (error: any) => {
        console.error('An error occurred while loading the article:', error);
      }
    );
  }

}
