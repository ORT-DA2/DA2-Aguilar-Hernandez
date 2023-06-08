import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ArticleService } from '../../_services/article.service';
import { Article } from '../../_type/article';
import { User } from '../../_type/user';
import { AuthenticationService } from '../../_services/authentication.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css'],
})
export class CreateArticleComponent {
  @Output() articlesUpdated = new EventEmitter<Article[]>();

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
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private articleService: ArticleService
  ) {}

  createArticle() {
    let article: Article = {
      dateLastModified: Date.now(),
      datePublished: Date.now(),
      id: '',
      isApproved: false,
      isEdited: false,
      offensiveContent: [],
      title: this.title,
      ownerId: this.getLoggedUser() || '',
      content: this.content,
      isPublic: this.isPublic,
      template: this.template,
      image: this.image,
    };

    this.articleService
      .createArticle(article)
      .subscribe((res) => console.log(res));
  }

  private getLoggedUser() {
    return localStorage.getItem('userId');
  }

  onChooseTemplate(template: string) {
    this.template = template;
  }
}
