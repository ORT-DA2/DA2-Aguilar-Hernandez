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

  getUsers(token: string | null): Observable<User[]> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .get<User[]>(`${environment.BASE_URL}${UserEndpoints.GET_USER}`, {
        headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }

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

  addUser(user: User, token: string | null): Observable<any> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .post<User>(`${environment.BASE_URL}${UserEndpoints.ADD_USER}`, user, {
        headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(error.error);
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

  editProfile(user: User | null, token: string | null): Observable<User> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    if (user === null) {
      return throwError('User is null');
    }
    return this.http
      .put<User>(
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

  deleteUser(userId: string, token: string | null): Observable<any> {
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', token);
    }
    return this.http
      .delete<any>(
        `${environment.BASE_URL}${UserEndpoints.EDIT_USER}/${userId}`,
        { headers }
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        })
      );
  }
}
