import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutorizationGuard } from 'src/app/guards/autorization.guard';
import { BugsComponent } from './bugs/bugs.component';
import { DashboardComponent } from './dashboard.component';
import { ProjectsComponent } from './projects/projects.component';
import { StartComponent } from './start/start.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  {
    path: '', component: DashboardComponent, children: [
      { path: '', component: StartComponent },
      {
        path: 'users',
       // canActivate: [AutorizationGuard],
        component: UsersComponent
      },
      {
        path: 'projects', component: ProjectsComponent,
        
      },
      {
        path: 'projects/:id', component: BugsComponent, data: {},
        
      },
      {
        path: 'bugs', component: BugsComponent, data: {}
      },
      {
        path: 'tasks',
       // canActivate: [AutorizationGuard],
        component: TasksComponent
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
