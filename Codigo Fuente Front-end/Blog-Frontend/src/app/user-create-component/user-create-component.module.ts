import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserCreateComponentComponent } from './user-create-component.component';
import { UserFormComponentModule } from '../user-form-component/user-form-component.module';

@NgModule({
  declarations: [UserCreateComponentComponent],
  imports: [CommonModule, UserFormComponentModule],
})
export class UserCreateComponentModule {}
