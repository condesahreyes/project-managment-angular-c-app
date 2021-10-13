/**Componentes */
import { SidenavComponent } from './components/sidenav/sidenav.component';
import {HomeComponent} from './components/home/home.component';
import { AppComponent } from './app.component';

/**Modules */
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { NgModule } from '@angular/core';
import { SessionService } from './services/session.service';

 //Sacar esto en un modulo de servicios
 import { HttpClientModule } from '@angular/common/http'; 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SidenavComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    HttpClientModule
  ],
  providers: [SessionService
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

