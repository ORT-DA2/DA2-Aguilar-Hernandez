import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, map } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Article } from '../_type/article';
import { environment } from '../environments/environment';
import { UserEndpoints } from '../_services/endpoints';
import { User } from '../_type/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  getUser(userId: string | null, token: string | null): Observable<User[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<User[]>(
        `${environment.BASE_URL}${UserEndpoints.GET_USER}/${userId}`,
        { headers }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  getRankingOffensive(
    startDate: Date,
    endDate: Date,
    token: string | null
  ): Observable<Array<[string, number]>> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Record<string, number>>(
        `${environment.BASE_URL}${UserEndpoints.GET_RANKING_OFFENSIVE}?startDate=${startDate}&endDate=${endDate}`,
        { headers }
      )
      .pipe(
        map((users: Record<string, number>) => {
          const entries = Object.entries(users);
          entries.sort((a, b) => b[1] - a[1]);
          return entries;
        }),
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  getRankingActivity(
    startDate: Date,
    endDate: Date,
    token: string | null
  ): Observable<Array<[string, number]>> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<Record<string, number>>(
        `${environment.BASE_URL}${UserEndpoints.GET_RANKING_ACTIVITY}?startDate=${startDate}&endDate=${endDate}`,
        { headers }
      )
      .pipe(
        map((users: Record<string, number>) => {
          const entries = Object.entries(users);
          entries.sort((a, b) => b[1] - a[1]);
          return entries;
        }),
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  editProfile(user: User, token: string | null): Observable<User[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .put<User[]>(
        `${environment.BASE_URL}${UserEndpoints.EDIT_USER}/${user.id}`,
        user,
        { headers }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }
}
