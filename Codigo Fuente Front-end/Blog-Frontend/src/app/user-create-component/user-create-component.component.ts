import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { UserService } from '../../_services/user.service';
import { User } from '../../_type/user';

declare let $: any;

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create-component.component.html',
  styleUrls: ['./user-create-component.component.css'],
})
export class UserCreateComponentComponent implements OnInit {
  @Output() userCreated = new EventEmitter<User>();
  @ViewChild('createUserModal') createUserModal!: ElementRef;
  token: string | null = '';
  errorMessage: string = '';

  newUser = {};

  constructor(private userService: UserService) {
    this.errorMessage = '';
  }

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
  }

  createUser(newUser: User) {
    this.userService.addUser(newUser, this.token).subscribe(
      () => {
        this.userCreated.emit(newUser);
      },
      (error: any) => {
        this.errorMessage = error.errorMessage;
      }
    );
  }

  openModal() {
    $(this.createUserModal.nativeElement).modal('show');
  }

  closeModal() {
    $(this.createUserModal.nativeElement).modal('hide');
  }
}
