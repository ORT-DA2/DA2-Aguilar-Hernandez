import { Component } from '@angular/core';
import { ArticleService } from '../../_services/article.service';
import { CommentService } from '../../_services/comment.service';
import { Article } from '../../_type/article';
import { Comment } from '../../_type/comment';

@Component({
  selector: 'app-moderate-articles-page',
  templateUrl: './moderate-articles-page.component.html',
  styleUrls: ['./moderate-articles-page.component.css'],
})
export class ModerateArticlesPageComponent {
  articles: Article[] = [];
  comments: Comment[] = [];
  token: string | null = null;

  constructor(
    private articleService: ArticleService,
    private commentService: CommentService
  ) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
    this.loadArticles();
    this.loadComments();
  }

  loadArticles(): void {
    this.articleService
      .getOffensiveArticles(this.token)
      .subscribe((articles: any) => {
        this.articles = articles.filter((article: any) => !article.isApproved);
      });
  }

  loadComments() {
    this.commentService.getComments(this.token).subscribe((comments: any) => {
      this.comments = comments;
    });
  }

  approve(article: any) {
    this.articleService
      .approveArticle(article, this.token)
      .subscribe((approvedArticle: any) => {
        const index = this.articles.indexOf(approvedArticle);
        if (index !== -1) {
          this.articles.splice(index, 1);
        }
      });
  }
}
