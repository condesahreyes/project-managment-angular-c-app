import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard.component';
import { StartComponent } from './start/start.component';
import { NavbarComponent } from './navbar/navbar.component';
import { UsersComponent } from './users/users.component';
import { ProjectsComponent } from './projects/projects.component';
import { BugsComponent } from './bugs/bugs.component';
import { UserFormComponent } from './users/user-form/user-form.component';
import { ProjectFormComponent } from './projects/project-form/project-form.component';
import { ProjectAssignFormComponent } from './projects/project-assign-form/project-assign-form.component';
import { BugFormComponent } from './bugs/bug-form/bug-form.component';
import { TasksComponent } from './tasks/tasks.component';
import { TaskFormComponent } from './tasks/task-form/task-form.component';


@NgModule({
  declarations: [
    DashboardComponent,
    StartComponent,
    NavbarComponent,
    UsersComponent,
    ProjectsComponent,
    BugsComponent,
    UserFormComponent,
    ProjectFormComponent,
    ProjectAssignFormComponent,
    BugFormComponent,
    TasksComponent,
    TaskFormComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class DashboardModule { }
