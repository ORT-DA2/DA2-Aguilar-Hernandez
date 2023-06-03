import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../../src/_services/user.service';
import { User } from '../../../src/_type/user';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponent implements OnInit {
  user: any;
  userId: string | null = null;
  token: string | null = null;
  editable: boolean = false;
  showPassword: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.token = localStorage.getItem('token');
    this.userId = localStorage.getItem('userId');
    this.userService.getUser(this.userId, this.token).subscribe((user: any) => {
      this.user = user;
    });
  }

  onEdit() {
    this.editable = true;
  }

  onSave() {
    this.userService.editProfile(this.user, this.token).subscribe(
      (updatedUser: User[]) => {
        this.user = updatedUser;
      },
      (error: any) => {
        console.error('An error occurred while editing the profile', error);
      },
      () => {
        console.log('Profile updated successfully');
      }
    );

    this.editable = false;
  }

  onChangePassword() {
    this.showPassword = !this.showPassword;
  }

  onCancelChangePassword() {
    this.showPassword = false;
  }

  onCancelEdit() {
    this.editable = false;
  }
}
