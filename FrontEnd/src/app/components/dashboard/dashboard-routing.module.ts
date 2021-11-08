import { ProjectViewComponent } from './projects/project-view/project-view.component';
import { ProjectsComponent } from './projects/projects.component';
import { DashboardComponent } from './dashboard.component';
import { StartComponent } from './start/start.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
import { BugsComponent } from './bugs/bugs.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AutorizationGuard } from 'src/app/guards/autorization.guard';

const routes: Routes = [
  {
    path: '', component: DashboardComponent, children: [
      { path: '', component: StartComponent },

      { path: 'users',
       // canActivate: [AutorizationGuard],
        component: UsersComponent},

      { path: 'projects', component: ProjectsComponent },

      { path: 'projects/:id', component: ProjectViewComponent, data: {} },

      { path: 'bugs', component: BugsComponent, data: {} },
      
      { path: 'tasks',
       // canActivate: [AutorizationGuard],
        component: TasksComponent,
        data: {} },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
