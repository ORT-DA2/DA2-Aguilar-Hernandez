import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { OffensiveRankingPageComponent } from './offensive-ranking-page/offensive-ranking-page.component';

import { AuthenticationService } from '../_services/authentication.service';
import { UserService } from '../_services/user.service';
import { OffensiveService } from '../_services/offensive.service';
import { ArticleService } from '../_services/article.service';
import { CreateArticleComponent } from './create-article/create-article.component';
import { ArticleProfileComponent } from './article-profile/article-profile.component';
import { ActivityRankingPageComponent } from './activity-ranking-page/activity-ranking-page.component';
import { UserManagementPageComponent } from './user-management-page/user-management-page.component';
import { UserCreateComponentComponent } from './user-create-component/user-create-component.component';
import { UserFormComponentComponent } from './user-form-component/user-form-component.component';
import { ModerateArticlesPageComponent } from './moderate-articles-page/moderate-articles-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginPageComponent,
    RegisterPageComponent,
    HomePageComponent,
    ProfilePageComponent,
    OffensiveRankingPageComponent,
    CreateArticleComponent,
    ArticleProfileComponent,
    ActivityRankingPageComponent,
    UserManagementPageComponent,
    UserCreateComponentComponent,
    UserFormComponentComponent,
    ModerateArticlesPageComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    AuthenticationService,
    UserService,
    OffensiveService,
    ArticleService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
