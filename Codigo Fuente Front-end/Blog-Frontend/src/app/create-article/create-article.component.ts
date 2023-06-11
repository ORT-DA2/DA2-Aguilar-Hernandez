import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ArticleService } from '../../_services/article.service';
import { Article } from '../../_type/article';
import { User } from '../../_type/user';
import { AuthenticationService } from '../../_services/authentication.service';
import { ActivatedRoute } from '@angular/router';
import {FormControl, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";

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
  template: string = '';
  image: string = '';
  image2: string = '';
  error: string = '';
  success: string = '';
  token: string | null = '';

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private articleService: ArticleService
  ) {}

  createArticle(article: Article) {
    article = {
      dateLastModified: Date.now(),
      datePublished: Date.now(),
      id: '',
      isApproved: false,
      isEdited: false,
      offensiveContent: [],
      title: this.title,
      owner: this.getLoggedUser() || '',
      content: this.content,
      isPublic: this.isPublic,
      template: this.template,
      image: this.image,
      image2:''
    };

    this.token = localStorage.getItem('token');
    this.articleService
      .createArticle(article, this.token)
      .subscribe((res) => this.success = 'Article successfully created',
      (error) => this.error = error.error);
    this.cleanFields();
  }



  private getLoggedUser() {
    return localStorage.getItem('userId');
  }

  onChooseTemplate(template: string) {
    this.template = template;
  }

  handleFileInput(event: any, isFirstImage: boolean) {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      const base64String = reader.result as string;
      const base64withoutMetadata = base64String.slice(base64String.indexOf(',') + 1);
      if(isFirstImage){
        this.image = base64withoutMetadata;
      }else{
        this.image2 = base64withoutMetadata;
      }
    };
    reader.readAsDataURL(file);
  }

  private cleanFields(){
    this.title = '';
    this. content = '';
    this.isPublic = true;
    this.template = '';
    this.image = '';
    this.image2 = '';
    this.error = '';
    this.token = null;
  }

}
