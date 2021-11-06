import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from './components/shared/shared.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthInterceptor} from "./services/session/auth-interceptor";

import { SessionService } from 'src/app/services/session/session.service';
import { UserService } from 'src/app/services/user/user.service';
import { AutorizationGuard } from './guards/autorization.guard';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    DashboardModule,
    FormsModule,
    HttpClientModule

  ],
  providers: [AutorizationGuard, SessionService, UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
