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

  onEdit() {}
}
