import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';
import { Credentials } from '../../_type/credentialsLogin';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  username: string = '';
  password: string = '';
  error: string = '';

  constructor(
    @Inject(AuthenticationService) private authService: AuthenticationService,
    private router: Router
  ) {}

  onLogin() {
    const credentials = {
      username: this.username,
      password: this.password,
    };

    this.authService.login(credentials).subscribe(
      () => {
        this.router.navigate(['/']);
      },
      (error) => {
        if (error.error && error.error.errorMessage === 'Invalid credentials') {
          this.error = 'Invalid username or password. Please try again.';
        } else {
          this.error = 'An error occurred during login. Please try again.';
        }
      }
    );
  }
}
