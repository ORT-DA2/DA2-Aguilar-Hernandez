import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportArticlePageComponent } from './import-article-page.component';

describe('ImportArticlePageComponent', () => {
  let component: ImportArticlePageComponent;
  let fixture: ComponentFixture<ImportArticlePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImportArticlePageComponent]
    });
    fixture = TestBed.createComponent(ImportArticlePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
