import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCreateComponentComponent } from './user-create-component.component';

describe('UserCreateComponentComponent', () => {
  let component: UserCreateComponentComponent;
  let fixture: ComponentFixture<UserCreateComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserCreateComponentComponent]
    });
    fixture = TestBed.createComponent(UserCreateComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
