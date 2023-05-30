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
  private token: string | null = null;

  constructor(private http: HttpClient) {
    const storedToken = localStorage.getItem('token') || null;
    this.username = localStorage.getItem('username') || '';
    if (storedToken) {
      this.token = storedToken;
      this.isLoggedIn.next(true);
      this.authStateChanged.next(true);
    }
  }

  public login(credentials: Credentials): Observable<any> {
    return this.http
      .post<any>(`${environment.BASE_URL}${AuthEndpoints.LOGIN}`, credentials)
      .pipe(
        catchError((error) => {
          return throwError(error);
        }),
        tap((response) => {
          const token = response.token;
          this.token = token;
          localStorage.setItem('token', token);
          this.isLoggedIn.next(true);
          this.username = response.user.username;
          localStorage.setItem('username', this.username);
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
    this.token = null;

    localStorage.removeItem('token');
    localStorage.removeItem('username');
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn.getValue();
  }

  getUsername(): string {
    return this.username;
  }

  getToken(): string | null{
    return this.token;
  }
}
