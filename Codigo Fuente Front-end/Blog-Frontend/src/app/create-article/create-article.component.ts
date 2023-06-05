import { Component } from '@angular/core';
import {ArticleService} from "../../_services/article.service";
import {Article} from "../../_type/article";
import {User} from "../../_type/user";
import {AuthenticationService} from "../../_services/authentication.service";

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css']
})
export class CreateArticleComponent {
  title: string = '';
  content: string = '';
  isPublic: boolean = true;
  ownerId: string = this.getLoggedUser() || '';
  datePublished: number = Date.now();
  dateLastModified: number = Date.now();
  template: string = '';
  image: string = '';
  error: string = '';
  isApproved: boolean = false;
  isEdited: boolean = false;
  offensiveContent: string[] = [];

  constructor(
    private authService: AuthenticationService,
    private ArticleService: ArticleService
  ){}

  onCreateArticle() {
    let article: Article = {
      dateLastModified: 0,
      datePublished: 0,
      id: "",
      isApproved: false,
      isEdited: false,
      offensiveContent: [],
      title: this.title,
      ownerId: this.getLoggedUser() || '',
      content: this.content,
      isPublic: this.isPublic,
      template: this.template,
      image: this.image
    };

    this.ArticleService.createArticle(article);

  }

  private getLoggedUser(){
    return localStorage.getItem('token');
  }
}
