import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from '../environments/environment';
import { AuthEndpoints } from '../_services/endpoints';
import { Credentials } from '../_type/credentialsLogin';
import { RegistrationCredentials } from '../_type/credentialsRegister';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public username: string = '';
  public authStateChanged: Subject<boolean> = new Subject<boolean>();

  constructor(private http: HttpClient) {}

  public login(credentials: Credentials): Observable<any> {
    return this.http
      .post<Credentials>(
        `${environment.BASE_URL}${AuthEndpoints.LOGIN}`,
        credentials
      )
      .pipe(
        catchError((error) => {
          return throwError(error);
        }),
        tap(() => {
          this.isLoggedIn.next(true);
          this.username = credentials.username;
          this.authStateChanged.next(true);
        })
      );
  }

  public register(credentials: RegistrationCredentials): Observable<any> {
    return this.http
      .post<RegistrationCredentials>(
        `${environment.BASE_URL}${AuthEndpoints.REGISTER}`,
        credentials
      )
      .pipe(
        catchError((error) => {
          return throwError(error.error);
        })
      );
  }

  logout() {
    this.isLoggedIn.next(false);
    this.username = '';
    this.authStateChanged.next(false);
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn.getValue();
  }
}
