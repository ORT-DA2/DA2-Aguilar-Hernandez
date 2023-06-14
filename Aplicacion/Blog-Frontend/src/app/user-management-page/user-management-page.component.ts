import { Component, ViewChild } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { User } from '../../_type/user';
import { Role } from '../../_type/role';
import { Router } from '@angular/router';
import { UserCreateComponentComponent } from '../user-create-component/user-create-component.component';

@Component({
  selector: 'app-user-management-page',
  templateUrl: './user-management-page.component.html',
  styleUrls: ['./user-management-page.component.css'],
})
export class UserManagementPageComponent {
  users: User[] = [];
  isEditing: boolean = false;
  token: string | null = '';
  roles: Role[] = [];
  errorMessage: string = '';

  @ViewChild(UserCreateComponentComponent)
  createUserComponent!: UserCreateComponentComponent;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.token = localStorage.getItem('token');
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers(this.token).subscribe((users: User[]) => {
      this.users = users.map((user) => ({ ...user, isEditing: false }));
    });
  }

  editUser(user: User) {
    this.users.forEach((u) => (u.isEditing = false));
    user.isEditing = true;
  }

  saveUser(user: User) {
    const requestBody = {
      id: user.id,
      firstName: user.firstName,
      lastName: user.lastName,
      username: user.username,
      password: user.password,
      roles: this.roles,
      email: user.email,
    };
    if (this.roles.length !== 0) {
      requestBody.roles = this.roles;
    } else {
      requestBody.roles = user.roles;
    }
    this.userService.editProfile(requestBody, this.token).subscribe(
      (userUpdated: User) => {
        user = userUpdated;
        console.log('User saved:', user);
        user.isEditing = false;
        window.location.reload();
      },
      (error: any) => {
        this.errorMessage = error.error.errorMessage;
      }
    );
  }

  cancelEdit(user: User) {
    user.isEditing = false;
  }

  deleteUser(user: User) {
    this.userService.deleteUser(user.id, this.token).subscribe(() => {});
  }

  createUser() {
    this.createUserComponent.openModal();
  }

  onUserCreated(newUser: User) {
    this.getUsers();
    this.createUserComponent.closeModal();
    window.location.reload();
  }
}
