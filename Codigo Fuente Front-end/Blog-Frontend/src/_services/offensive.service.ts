import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { OffensiveEndpoints } from '../_services/endpoints';
import { OffensiveWord } from '../_type/offensiveWord';

@Injectable({
  providedIn: 'root',
})
export class OffensiveService {
  constructor(private http: HttpClient) {}

  getOffensive(): Observable<OffensiveWord[]> {
    const authorization = localStorage.getItem('token') || '';
    const headers = new HttpHeaders().set('Authorization', authorization);
    return this.http
      .get<OffensiveWord[]>(
        `${environment.BASE_URL}${OffensiveEndpoints.GET_OFFENSIVE}`,
        {
          headers,
        }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  addOffensive(word: string | undefined): Observable<OffensiveWord> {
    if (!word?.trim()) {
      return of({ error: 'The word cannot be empty' });
    }
    const authorization = localStorage.getItem('token') || '';
    const headers = new HttpHeaders().set('Authorization', authorization);
    return this.http
      .post<any>(
        `${environment.BASE_URL}${OffensiveEndpoints.ADD_OFFENSIVE}`,
        { word },
        {
          headers,
        }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  removeOffensive(word: string): Observable<OffensiveWord> {
    const authorization = localStorage.getItem('token') || '';
    const headers = new HttpHeaders().set('Authorization', authorization);
    return this.http
      .delete<any>(
        `${environment.BASE_URL}${OffensiveEndpoints.REMOVE_OFFENSIVE}/${word}`,
        {
          headers,
        }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }
}
