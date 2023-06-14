import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModerateArticlesPageComponent } from './moderate-articles-page.component';

describe('ModerateArticlesPageComponent', () => {
  let component: ModerateArticlesPageComponent;
  let fixture: ComponentFixture<ModerateArticlesPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModerateArticlesPageComponent]
    });
    fixture = TestBed.createComponent(ModerateArticlesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
