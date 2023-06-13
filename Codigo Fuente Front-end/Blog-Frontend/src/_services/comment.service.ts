import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {environment} from "../environments/environment";
import {CommentEndpoints} from "./endpoints";
import {catchError} from "rxjs/operators";
import {Comment} from "../_type/comment";
import {CommentReply} from "../_type/CommentReply";

@Injectable({
  providedIn: 'root',
})

export class CommentService{
  constructor(private http: HttpClient) {}

  createComment(comment: Comment,
                token: string | null): Observable<Comment> {
    let headers = new HttpHeaders();
    if(token){
      headers = headers.set('Authorization', token);
    }
    return this.http
      .post<Comment>(
        `${environment.BASE_URL}${CommentEndpoints.ADD_COMMENT}`,
        comment, {headers}
      )
      .pipe(catchError(this.handleError));
  }

  replyComment(reply: CommentReply, token: string|null):Observable<CommentReply>{
    let headers = new HttpHeaders();
    if(token){
      headers = headers.set('Authorization', token);
    }
    return this.http
      .put<CommentReply>(
        `${environment.BASE_URL}${CommentEndpoints.REPLY_COMMENT}`,
        reply, {headers}
      )
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }

}
