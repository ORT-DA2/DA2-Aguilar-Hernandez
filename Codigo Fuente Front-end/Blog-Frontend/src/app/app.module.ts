import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { CreateArticleComponent } from './create-article/create-article.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { OffensiveRankingPageComponent } from './offensive-ranking-page/offensive-ranking-page.component';

import { AuthenticationService } from '../_services/authentication.service';
import { UserService } from '../_services/user.service';
import { OffensiveService } from '../_services/offensive.service';
import { ArticleService } from '../_services/article.service';
import { ActivityRankingPageComponent } from './activity-ranking-page/activity-ranking-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginPageComponent,
    RegisterPageComponent,
    HomePageComponent,
    ProfilePageComponent,
    CreateArticleComponent,
    OffensiveRankingPageComponent,
    ActivityRankingPageComponent,
  ],

  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [
    AuthenticationService,
    UserService,
    OffensiveService,
    ArticleService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
