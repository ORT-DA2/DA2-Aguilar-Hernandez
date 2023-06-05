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

import { AuthenticationService } from '../_services/authentication.service';
import { UserService } from '../_services/user.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginPageComponent,
    RegisterPageComponent,
    HomePageComponent,
    ProfilePageComponent,
  ],

  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [AuthenticationService, UserService],
  bootstrap: [AppComponent],
})
export class AppModule {}
