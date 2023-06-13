import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { CreateArticleComponent } from './create-article/create-article.component';
import { OffensiveRankingPageComponent } from './offensive-ranking-page/offensive-ranking-page.component';
import { ActivityRankingPageComponent } from './activity-ranking-page/activity-ranking-page.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'profile/:id', component: ProfilePageComponent },
  { path: 'create-article', component: CreateArticleComponent },
  { path: 'offensive-ranking', component: OffensiveRankingPageComponent },
  { path: 'activity-ranking', component: ActivityRankingPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
