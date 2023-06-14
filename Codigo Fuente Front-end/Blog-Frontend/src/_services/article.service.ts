import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Article } from '../_type/article';
import { environment } from '../environments/environment';
import { ArticleEndpoints } from '../_services/endpoints';
import { Import } from '../_type/import';

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
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  getPublicArticles(token: string | null): Observable<Article[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Article[]>(
        `${environment.BASE_URL}${ArticleEndpoints.ALL_PUBLIC_ARTICLES}`,
        { headers }
      )
      .pipe(
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  getOffensiveArticles(token: string | null): Observable<Article[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Article[]>(
        `${environment.BASE_URL}${ArticleEndpoints.ALL_OFFENSIVE_ARTICLES}`,
        { headers }
      )
      .pipe(
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  getImporters(): Observable<Article[]> {
    return this.http
      .get<any>(`${environment.BASE_URL}${ArticleEndpoints.IMPORTERS}`)
      .pipe(
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  sendImporters(importer: Import, token: string | null): Observable<Article[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .post<any>(
        `${environment.BASE_URL}${ArticleEndpoints.IMPORT}`,
        importer,
        {
          headers,
        }
      )
      .pipe(
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  getUserArticles(
    token: string | null,
    username: string | undefined
  ): Observable<Article[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Article[]>(
        `${environment.BASE_URL}${ArticleEndpoints.ALL_USER_ARTICLES}?username=${username}`,
        { headers }
      )
      .pipe(
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  updateArticle(article: Article, token: string | null): Observable<Article> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .put<Article>(
        `${environment.BASE_URL}${ArticleEndpoints.ARTICLES}/${article.id}`,
        article,
        { headers }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  getArticleById(guid: string, token: string | null): Observable<Article> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Article>(
        `${environment.BASE_URL}${ArticleEndpoints.ARTICLES}/${guid}`,
        { headers }
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
        catchError((error: any) => {
          return throwError(error);
        })
      );
  }

  createArticle(article: Article, token: string | null): Observable<Article> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .post<Article>(
        `${environment.BASE_URL}${ArticleEndpoints.ARTICLES}`,
        article,
        { headers }
      )
      .pipe(catchError(this.handleError));
  }

  approveArticle(article: Article, token: string | null): Observable<Article> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .put<Article>(
        `${environment.BASE_URL}${ArticleEndpoints.APPROVE_ARTICLE}/${article.id}`,
        article,
        { headers }
      )
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }
}
