import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
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
          console.error('An error occurred while fetching user', error);
          return throwError(error);
        })
      );
  }
}
