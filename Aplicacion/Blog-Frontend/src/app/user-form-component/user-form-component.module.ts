import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserFormComponentComponent } from './user-form-component.component';

@NgModule({
  declarations: [UserFormComponentComponent],
  imports: [CommonModule],
  exports: [UserFormComponentComponent],
})
export class UserFormComponentModule {}
