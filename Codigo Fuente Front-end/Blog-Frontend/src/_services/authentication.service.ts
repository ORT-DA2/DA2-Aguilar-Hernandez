import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable, throwError } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { environment } from '../environments/environment';
import { AuthEndpoints } from '../_services/endpoints';
import { Credentials } from '../_type/credentialsLogin';
import { RegistrationCredentials } from '../_type/credentialsRegister';
import { catchError, tap } from 'rxjs/operators';
import { UserEndpoints } from '../_services/endpoints';
import { User } from '../_type/user';
import { Role } from '../_type/role';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  isLoggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  user$ = new BehaviorSubject<User | null>(null);
  public authStateChanged: Subject<boolean> = new Subject<boolean>();
  private token: string | null = null;
  admin: boolean | undefined = false;

  constructor(private http: HttpClient) {
    const storedToken = localStorage.getItem('token') || null;
    if (storedToken) {
      this.token = storedToken;
      this.isLoggedIn$.next(true);
      this.authStateChanged.next(true);
      this.getUser();
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
          this.user$.next(response.user);
          localStorage.setItem('token', token);
          localStorage.setItem('userId', response.user.id);
          this.getUser();
          this.isLoggedIn$.next(true);
          this.authStateChanged.next(true);
          this.admin = this.user$
            .getValue()
            ?.roles.some((role) => role.role === 1);
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
    this.isLoggedIn$.next(false);
    this.authStateChanged.next(false);
    this.user$.next(null);
    this.token = null;

    localStorage.removeItem('token');
    localStorage.removeItem('userId');
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn$.getValue();
  }

  public getUser() {
    if (this.isLoggedIn$.getValue()) {
      const authorization = localStorage.getItem('token') || '';
      const headers = new HttpHeaders().set('Authorization', authorization);
      const userId = localStorage.getItem('userId') || '';
      this.http
        .get<any>(
          `${environment.BASE_URL}${UserEndpoints.GET_USER}/${userId}`,
          {
            headers,
          }
        )
        .subscribe((user) => {
          this.user$.next(user);
        });
    }
  }
}
