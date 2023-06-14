import { Component, EventEmitter, Output, Input } from '@angular/core';
import { ArticleService } from '../../_services/article.service';
import { Article } from '../../_type/article';
import { AuthenticationService } from '../../_services/authentication.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css'],
})
export class CreateArticleComponent {
  @Input() mode: 'create' | 'edit' = 'create';
  @Input() article: Article | undefined;
  @Output() articleCreated = new EventEmitter<Article>();
  @Output() articleUpdated = new EventEmitter<Article>();

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

  ngOnInit(): void {
    if (this.mode === 'edit' && this.article) {
      this.title = this.article.title;
      this.content = this.article.content;
      this.isPublic = this.article.isPublic;
      this.template = this.article.template;
      this.image = this.article.image;
      this.image2 = this.article.image2;
    }
  }

  onFormSubmit(formData: any): void {
    if (this.mode === 'create') {
      this.createArticle(formData);
    } else if (this.mode === 'edit') {
      this.updateArticle();
    }
  }

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
      image2: this.image2,
    };

    this.token = localStorage.getItem('token');
    this.articleService.createArticle(article, this.token).subscribe(
      (res) => (this.success = 'Article successfully created'),
      (error) => (this.error = error.error)
    );
    this.cleanFields();
  }

  private getLoggedUser() {
    return localStorage.getItem('userId');
  }

  onChooseTemplate(template: string) {
    this.template = template;
  }

  handleFirstImageInput(event: any) {
    this.readImage(event).then((data: string) => {
      this.image = data;
    });
  }

  handleSecondImageInput(event: any) {
    this.readImage(event).then((data: string) => {
      this.image2 = data;
    });
  }

  readImage(event: any): Promise<string> {
    return new Promise((resolve, reject) => {
      const file: File = event.target.files[0];
      const reader = new FileReader();

      reader.onloadend = () => {
        const base64String = reader.result as string;
        resolve(base64String.slice(base64String.indexOf(',') + 1));
      };

      reader.onerror = reject;

      reader.readAsDataURL(file);
    });
  }

  updateArticle() {
    if (!this.article) {
      return;
    }

    const updatedArticle: Article = {
      ...this.article,
      title: this.title,
      content: this.content,
      isPublic: this.isPublic,
      template: this.template,
    };

    if (this.image !== this.article.image) {
      updatedArticle.image = this.image;
    }

    if (this.image2 !== this.article.image2) {
      updatedArticle.image2 = this.image2;
    }

    this.token = localStorage.getItem('token');
    this.articleService.updateArticle(updatedArticle, this.token).subscribe(
      (res: any) => {
        this.article = updatedArticle;
        this.success = 'Article successfully updated';
        this.articleUpdated.emit(res);
        this.cleanFields();
      },
      (error: any) => (this.error = error.error)
    );
  }

  private cleanFields() {
    this.title = '';
    this.content = '';
    this.isPublic = true;
    this.template = '';
    this.image = '';
    this.image2 = '';
    this.error = '';
    this.token = null;
  }
}
