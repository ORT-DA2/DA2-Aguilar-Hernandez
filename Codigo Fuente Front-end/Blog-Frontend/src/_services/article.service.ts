import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import { Article } from '../_type/article';
import { environment } from '../environments/environment';
import { ArticleEndpoints } from '../_services/endpoints';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private http: HttpClient) {}

  getLastArticles(): Observable<Article[]> {
    return this.http
      .get<Article[]>(
        `${environment.BASE_URL}${ArticleEndpoints.LAST_ARTICLES}`
      )
      .pipe(
        catchError((error) => {
          console.error(
            'An error occurred while fetching last articles:',
            error
          );
          return throwError(error);
        })
      );
  }

  createArticle(article : Article): void{
    this.http
      .post<Article>(`${environment.BASE_URL}${ArticleEndpoints.CREATE_ARTICLE}`, article)
      .pipe(catchError((error) => throwError(error)));
  }
}
