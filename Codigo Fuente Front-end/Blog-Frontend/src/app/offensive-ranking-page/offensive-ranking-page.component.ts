import { Component } from '@angular/core';
import { OffensiveService } from '../../_services/offensive.service';
import { UserService } from '../../_services/user.service';
import { OffensiveWord } from '../../_type/offensiveWord';
import { User } from '../../_type/user';

@Component({
  selector: 'app-offensive-ranking-page',
  templateUrl: './offensive-ranking-page.component.html',
  styleUrls: ['./offensive-ranking-page.component.css'],
})
export class OffensiveRankingPageComponent {
  offensiveWords: OffensiveWord[] = [];
  filteredUsers: Array<[string, number]> = [];
  newWord: OffensiveWord = { id: '', word: '', error: '' };
  errorMessage = '';
  startDate = new Date('2023-01-01');
  endDate = new Date('2023-12-31');
  token: string | null = '';

  constructor(
    private offensiveService: OffensiveService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadOffensiveWords();
  }

  loadOffensiveWords(): void {
    this.token = localStorage.getItem('token');
    this.offensiveService.getOffensive().subscribe(
      (words: any) => {
        this.offensiveWords = words;
      },
      (error: any) => {
        console.error('An error occurred while loading offensive words', error);
      }
    );
  }

  filterRanking(): void {
    this.errorMessage = '';
    if (!this.datesValidation()) {
      return;
    }
    this.userService
      .getRankingOffensive(this.startDate, this.endDate, this.token)
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

  removeWord(word: string) {
    this.offensiveService.removeOffensive(word).subscribe(
      (response: any) => {
        this.offensiveWords = this.offensiveWords.filter(
          (offensiveWord) => offensiveWord.word !== word
        );
      },
      (error: any) => {
        this.errorMessage = error.error.errorMessage;
      }
    );
  }

  addWord() {
    this.offensiveService.addOffensive(this.newWord.word).subscribe(
      (response: OffensiveWord) => {
        if (response.error) {
          this.errorMessage = response.error;
        } else {
          this.offensiveWords.push(response);
        }
        this.newWord.word = '';
      },
      (error: any) => {
        this.errorMessage = error.error.errorMessage;
      }
    );
  }
}
