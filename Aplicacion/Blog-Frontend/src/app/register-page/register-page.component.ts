import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
})
export class RegisterPageComponent {
  firstName: string = '';
  lastName: string = '';
  username: string = '';
  password: string = '';
  email: string = '';
  error: string = '';

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  onRegister() {
    const credentials = {
      firstName: this.firstName,
      lastName: this.lastName,
      username: this.username,
      password: this.password,
      email: this.email,
    };

    this.authService.register(credentials).subscribe(
      () => {
        this.router.navigate(['/login']);
      },
      (error: any) => {
        this.error = error.errorMessage;
      }
    );
  }
}
