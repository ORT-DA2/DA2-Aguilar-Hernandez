import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './profile-page.component';
import { ArticleCardComponent } from '../article-card/article-card.component';

@NgModule({
  declarations: [ProfilePageComponent, ArticleCardComponent],
  imports: [CommonModule],
  exports: [ProfilePageComponent],
})
export class UserProfileModule {}
