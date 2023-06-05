import { Component } from '@angular/core';
import { OffensiveService } from '../../_services/offensive.service';
import { OffensiveWord } from '../../_type/offensiveWord';

@Component({
  selector: 'app-offensive-ranking-page',
  templateUrl: './offensive-ranking-page.component.html',
  styleUrls: ['./offensive-ranking-page.component.css'],
})
export class OffensiveRankingPageComponent {
  offensiveWords: OffensiveWord[] = [];
  newWord: OffensiveWord = { id: '', word: '', error: '' };
  errorMessage = '';

  constructor(private offensiveService: OffensiveService) {}

  ngOnInit(): void {
    this.loadOffensiveWords();
  }

  loadOffensiveWords(): void {
    this.offensiveService.getOffensive().subscribe(
      (words: any) => {
        this.offensiveWords = words;
      },
      (error: any) => {
        console.error('An error occurred while loading offensive words', error);
      }
    );
  }

  removeWord(word: string) {
    this.offensiveService.removeOffensive(word).subscribe(
      (response: any) => {
        this.offensiveWords = this.offensiveWords.filter(
          (offensiveWord) => offensiveWord.word !== word
        );
      },
      (error: any) => {
        console.error(
          'An error occurred while removing offensive words',
          error
        );
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
        console.error('An error occurred while adding offensive words', error);
      }
    );
  }
}
