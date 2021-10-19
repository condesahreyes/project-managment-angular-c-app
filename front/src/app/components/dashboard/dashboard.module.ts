import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard.component';
import { StartComponent } from './start/start.component';
import { NavbarComponent } from './navbar/navbar.component';
import { UsersComponent } from './users/users.component';
import { ProjectsComponent } from './projects/projects.component';


@NgModule({
  declarations: [
    DashboardComponent,
    StartComponent,
    NavbarComponent,
    UsersComponent,
    ProjectsComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class DashboardModule { }
