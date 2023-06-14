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
import { HeaderComponent } from './header/header.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ImportArticlePageComponent } from './import-article-page/import-article-page.component';
import { AuthenticationGuard } from '../_guards/authentication.guard';
import { AdminGuard } from '../_guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    component: HeaderComponent,
    outlet: 'header',
  },
  { path: '', component: HomePageComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  {
    path: 'profile/:id',
    component: ProfilePageComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'profile/:username',
    component: ProfilePageComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'create-article',
    component: CreateArticleComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'offensive-ranking',
    component: OffensiveRankingPageComponent,
    canActivate: [AdminGuard],
  },
  {
    path: 'articles/:id',
    component: ArticleProfileComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'activity-ranking',
    component: ActivityRankingPageComponent,
    canActivate: [AdminGuard],
  },
  {
    path: 'user-management',
    component: UserManagementPageComponent,
  },
  {
    path: 'articles-management',
    component: ModerateArticlesPageComponent,
    canActivate: [AdminGuard],
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: 'importers',
    component: ImportArticlePageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
