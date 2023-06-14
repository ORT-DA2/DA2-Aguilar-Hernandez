import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../_type/user';
import { Role } from '../../_type/role';

@Component({
  selector: 'app-user-form-component',
  templateUrl: './user-form-component.component.html',
  styleUrls: ['./user-form-component.component.css'],
})
export class UserFormComponentComponent implements OnInit {
  @Output() userCreated = new EventEmitter<User>();
  user: User = {
    id: '',
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    roles: [],
    email: '',
  };

  constructor() {}

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.userCreated.emit(this.user);
      form.reset();
    }
  }
}
