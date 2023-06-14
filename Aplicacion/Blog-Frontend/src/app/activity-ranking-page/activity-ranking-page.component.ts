import { Component } from '@angular/core';
import { UserService } from '../../_services/user.service';

@Component({
  selector: 'app-activity-ranking-page',
  templateUrl: './activity-ranking-page.component.html',
  styleUrls: ['./activity-ranking-page.component.css'],
})
export class ActivityRankingPageComponent {
  errorMessage = '';
  startDate = new Date('2023-01-01');
  endDate = new Date('2023-12-31');
  token: string | null = '';
  filteredUsers: Array<[string, number]> = [];

  constructor(private userService: UserService) {
    this.token = localStorage.getItem('token');
  }

  filterRanking(): void {
    this.errorMessage = '';
    if (!this.datesValidation()) {
      return;
    }
    this.userService
      .getRankingActivity(this.startDate, this.endDate, this.token)
      .subscribe(
        (response: any) => {
          this.filteredUsers = response;
        },
        (error: any) => {
          this.errorMessage = error.error.errorMessage;
        }
      );
  }

  private datesValidation(): boolean {
    if (this.startDate > this.endDate) {
      this.errorMessage = 'Start date must be before end date';
    }
    if (this.startDate > new Date()) {
      this.errorMessage = 'Start date must be before today';
    }
    if (this.endDate > new Date()) {
      this.errorMessage = 'End date must be before today';
    }
    return !this.errorMessage;
  }
}
