import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { AuthenticationService } from '../_services/authentication.service';
import { RegisterPageComponent } from './register-page/register-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { CreateArticleComponent } from './create-article/create-article.component';

@NgModule({
  declarations: [AppComponent, HeaderComponent, LoginPageComponent, RegisterPageComponent, HomePageComponent, CreateArticleComponent],
  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [AuthenticationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
