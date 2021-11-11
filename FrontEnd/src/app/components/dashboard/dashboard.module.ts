import { DashboardRoutingModule } from './dashboard-routing.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectAssignFormComponent } from './projects/project-assign-form/project-assign-form.component';
import { ProjectViewComponent } from './projects/project-view/project-view.component';
import { ProjectFormComponent } from './projects/project-form/project-form.component';
import { UserFormComponent } from './users/user-form/user-form.component';
import { TaskFormComponent } from './tasks/task-form/task-form.component';
import { BugFormComponent } from './bugs/bug-form/bug-form.component';
import { ProjectsComponent } from './projects/projects.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { DashboardComponent } from './dashboard.component';
import { StartComponent } from './start/start.component';
import { UsersComponent } from './users/users.component';
import { TasksComponent } from './tasks/tasks.component';
import { SharedModule } from '../shared/shared.module';
import { BugsComponent } from './bugs/bugs.component';
import { BugImportFormComponent } from './bugs/bug-import-form/bug-import-form.component';

@NgModule({
  declarations: [
    DashboardComponent,
    StartComponent,
    UsersComponent,
    ProjectsComponent,
    BugsComponent,
    UserFormComponent,
    ProjectFormComponent,
    ProjectAssignFormComponent,
    BugFormComponent,
    TasksComponent,
    TaskFormComponent,
    NavbarComponent,
    ProjectViewComponent,
    BugImportFormComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class DashboardModule { }