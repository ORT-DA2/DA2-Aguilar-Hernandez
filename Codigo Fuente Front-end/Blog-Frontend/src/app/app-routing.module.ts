import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { CreateArticleComponent } from './create-article/create-article.component';
import { OffensiveRankingPageComponent } from './offensive-ranking-page/offensive-ranking-page.component';
import { ArticleProfileComponent } from './article-profile/article-profile.component';
import { ActivityRankingPageComponent } from './activity-ranking-page/activity-ranking-page.component';
import { UserManagementPageComponent } from './user-management-page/user-management-page.component';
import { ModerateArticlesPageComponent } from './moderate-articles-page/moderate-articles-page.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'profile/:id', component: ProfilePageComponent },
  { path: 'profile/:username', component: ProfilePageComponent },
  { path: 'create-article', component: CreateArticleComponent },
  { path: 'offensive-ranking', component: OffensiveRankingPageComponent },
  { path: 'articles/:id', component: ArticleProfileComponent },
  { path: 'activity-ranking', component: ActivityRankingPageComponent },
  { path: 'user-management', component: UserManagementPageComponent },
  { path: 'articles-management', component: ModerateArticlesPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
