import { Component } from '@angular/core';
import {ArticleService} from "../../_services/article.service";
import {Article} from "../../_type/article";
import { ActivatedRoute } from '@angular/router';
import {Comment} from "../../_type/comment";
import {CommentService} from "../../_services/comment.service";
import {CommentReply} from "../../_type/CommentReply";

@Component({
  selector: 'app-article-profile',
  templateUrl: './article-profile.component.html',
  styleUrls: ['./article-profile.component.css']
})
export class ArticleProfileComponent {

  id!: string;
  article: Article = {} as Article;
  token: string | null = null;
  loggedUser: string | null = null;
  comments: Comment[] = [];
  addingComment: boolean = false;
  commentInput!: string;
  errorMessage?: string;
  replyingComment: boolean = false;
  replyInput!: string;
  indexCommentReplying!: number;

  constructor(private articleService: ArticleService,
              private commentService: CommentService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void{
    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.token = localStorage.getItem('token');
    this.loggedUser = localStorage.getItem('username');
    this.loadArticle();
  }

  private loadArticle():void{
    this.articleService.getArticleById(this.id, this.token).subscribe(
      (article: Article)=>{
        this.article = article;
        this.loadComments(article);
      },
      (error: any) => {
        console.error('An error occurred while loading the article:', error);
      }
    );
  }

  private loadComments(article: Article):void{
    if(article.comments){
      article.comments.forEach(c=>this.comments?.push(c));
    }
  }

  onAddComment():void{
    this.addingComment = !this.addingComment;
  }

  onReplyComment(): void{
    this.replyingComment = !this.replyingComment;
  }

  createComment():void{
    const comment: Comment = {
      datePublished: Date.now(),
      id: "",
      isApproved: false,
      isEdited: false,
      isPublic: true,
      offensiveWords: [],
      ownerUsername: this.loggedUser,
      body: this.commentInput,
      articleId: this.article.id
    }

    this.commentService.createComment(comment, this.token)
      .subscribe(c=> this.onAddComment(),
        error => this.errorMessage = error.error);
    this.comments.push(comment);
    this.commentInput = '';
  }

  loggedUserIsAuthor(): boolean{
    return this.loggedUser == this.article.owner;
  }

  createReply(commentId: string, commentIndex: number): void{
    const reply: CommentReply = {
      reply: this.replyInput,
      commentId: commentId
    }
    this.commentService.replyComment(reply, this.token)
      .subscribe(r=>this.onReplyComment(),
        error => this.errorMessage = error.error);
    this.comments[commentIndex].reply = this.replyInput;
    this.replyInput = '';
  }

  setIndexCommentReplying(index: number){
    this.indexCommentReplying = index;
  }

}
