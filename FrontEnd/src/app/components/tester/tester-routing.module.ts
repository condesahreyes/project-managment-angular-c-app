import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BugsComponent } from '../dashboard/bugs/bugs.component';
import { StartComponent } from '../dashboard/start/start.component';

const routes: Routes = [
  { path: '', component: StartComponent },
  {
    path: 'bugs', component: BugsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class TesterRoutingModule { }
