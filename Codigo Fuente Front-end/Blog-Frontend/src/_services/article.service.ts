import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
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
          return throwError(error);
        })
      );
  }

  searchArticles(
    searchTerm: string,
    token: string | null
  ): Observable<Article[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Article[]>(
        `${environment.BASE_URL}${ArticleEndpoints.SEARCH_ARTICLES}?text=${searchTerm}`,
        { headers }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }
}
