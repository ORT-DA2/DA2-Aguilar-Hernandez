import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityRankingPageComponent } from './activity-ranking-page.component';

describe('ActivityRankingPageComponent', () => {
  let component: ActivityRankingPageComponent;
  let fixture: ComponentFixture<ActivityRankingPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActivityRankingPageComponent]
    });
    fixture = TestBed.createComponent(ActivityRankingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
