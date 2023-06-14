import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OffensiveRankingPageComponent } from './offensive-ranking-page.component';

describe('OffensiveRankingPageComponent', () => {
  let component: OffensiveRankingPageComponent;
  let fixture: ComponentFixture<OffensiveRankingPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OffensiveRankingPageComponent]
    });
    fixture = TestBed.createComponent(OffensiveRankingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
