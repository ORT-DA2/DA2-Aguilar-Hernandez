import { Component } from '@angular/core';
import { ArticleService } from '../../_services/article.service';
import { Article } from '../../_type/article';
import { User } from '../../_type/user';
import { ActivatedRoute } from '@angular/router';
import { Comment } from '../../_type/comment';
import { CommentService } from '../../_services/comment.service';
import { UserService } from '../../_services/user.service';
import { CommentReply } from '../../_type/CommentReply';

@Component({
  selector: 'app-article-profile',
  templateUrl: './article-profile.component.html',
  styleUrls: ['./article-profile.component.css'],
})
export class ArticleProfileComponent {
  id!: string;
  userId: string | null = '';
  article: Article = {} as Article;
  token: string | null = null;
  loggedUser: User | null = null;
  comments: Comment[] = [];
  addingComment: boolean = false;
  commentInput!: string;
  errorMessage?: string;
  replyingComment: boolean = false;
  replyInput!: string;
  indexCommentReplying!: number;
  isOffensive: boolean = false;
  isEditing: boolean = false;

  constructor(
    private articleService: ArticleService,
    private commentService: CommentService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.token = localStorage.getItem('token');
    this.userId = localStorage.getItem('userId');
    this.userService.getUser(this.userId, this.token).subscribe((user) => {
      this.loggedUser = user;
    });
    this.loadArticle();
  }

  private loadArticle(): void {
    this.articleService.getArticleById(this.id, this.token).subscribe(
      (article: Article) => {
        this.article = article;
        this.loadComments(article);
      },
      (error: any) => {
        console.error('An error occurred while loading the article:', error);
      }
    );
  }

  private loadComments(article: Article): void {
    if (article.comments) {
      article.comments.forEach((c) => {
        this.comments?.push(c);
      });
      article.comments.forEach((c) => {
        this.checkOffensive(c);
      });
    }
  }

  checkOffensive(comment: Comment) {
    if (comment.offensiveContent && comment.offensiveContent.length > 0) {
      comment.isOffensive = true;
    }
  }

  onAddComment(): void {
    this.addingComment = !this.addingComment;
  }

  onReplyComment(): void {
    this.replyingComment = !this.replyingComment;
  }

  loggedUserIsAuthor(): boolean {
    return this.loggedUser?.username == this.article.owner;
  }

  loggedUserIsAdmin(): boolean | undefined {
    return this.loggedUser?.roles.some((role: any) => role.role === 1);
  }

  createComment(): void {
    const comment: Comment = {
      datePublished: Date.now(),
      id: '',
      isApproved: false,
      isEdited: false,
      isPublic: true,
      offensiveContent: [],
      ownerUsername: this.loggedUser?.username,
      body: this.commentInput,
      articleId: this.article.id,
      isOffensive: false,
    };

    this.commentService.createComment(comment, this.token).subscribe(
      (c) => this.onAddComment(),
      (error) => (this.errorMessage = error.error)
    );
    this.checkOffensive(comment);
    this.comments.push(comment);
    this.commentInput = '';
  }

  createReply(commentId: string, commentIndex: number): void {
    const reply: CommentReply = {
      reply: this.replyInput,
      commentId: commentId,
    };
    this.commentService.replyComment(reply, this.token).subscribe(
      (r) => this.onReplyComment(),
      (error) => (this.errorMessage = error.error)
    );
    this.comments[commentIndex].reply = this.replyInput;
    this.replyInput = '';
  }

  setIndexCommentReplying(index: number) {
    this.indexCommentReplying = index;
  }

  onEdit() {
    this.isEditing = !this.isEditing;
  }

  onArticleUpdated(updatedArticle: Article): void {
    this.isEditing = false;
    this.article = updatedArticle;
  }
}
