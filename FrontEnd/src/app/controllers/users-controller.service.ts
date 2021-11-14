import { DeveloperControllerService } from './developer-controller/developer-controller.service';
import { TesterControllerService } from './tester-controller/tester-controller.service';
import { ProjectService } from '../services/project/project.service';
import { SessionService } from '../services/session/session.service';
import { UserEntryModel } from '../models/users/UserEntryModel';
import { TaskService } from '../services/task/task.service';
import { ProjectOut } from '../models/project/ProjectOut';
import { constants } from '../components/shared/constans';
import { BugService } from '../services/bug/bug.service';
import { Injectable } from '@angular/core';
import { Bug } from '../models/bug/Bug';
import { Observable } from 'rxjs';
import { BugState } from '../models/bug/BugState';
import { Task } from '../models/task/task';


@Injectable({
  providedIn: 'root'
})
export class UsersControllerService{

  rolDeveloper = 'Desarrollador';
  rolTester = 'Tester';

  user: any;

  constructor(
    private testerController: TesterControllerService,
    private developerController: DeveloperControllerService,
    private sessionService: SessionService,
    private projectService: ProjectService,
    private bugService: BugService,
    private taskService: TaskService
  ) { }


  saveUserLogued() {
    this.sessionService.getUserLogged().subscribe(u => {
      this.user = u
    });
  }

  getUserLogued(): UserEntryModel {
    return this.user;
  }

  getBugs(): Observable<Bug[]> {
    this.saveUserLogued();
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester) {
      return this.testerController.getBugs(this.user);
    } else if (userRol === this.rolDeveloper) {
      return this.developerController.getBugs(this.user);
    }

    return this.bugService.getBugs();
  }

  getBugsColumns(): string[] {
    return constants.bugColumns;
  }

  getActionsBugs(): string[] {
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester) {
      return constants.bugActionsTester;
    }
    else if (userRol === this.rolDeveloper) {
      return constants.bugActionsDeveloper;
    }

    return constants.bugActions;
  }

  getProjects(): Observable<ProjectOut[]> {
    this.saveUserLogued();
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester) {
      return this.testerController.getProjects(this.user);
    } else if (userRol === this.rolDeveloper) {
      return this.developerController.getProjects(this.user);
    }

    return this.projectService.getProjects();
  }

  getProjectColumns(): string[] {
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester || userRol === this.rolDeveloper) {
      return constants.projectColumnsDeveloperTester;
    }

    return constants.projectColumns;
  }

  getActionsProject(): string[] {
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester || userRol === this.rolDeveloper) {
      return constants.projectActionsDeveloperTester;
    }

    return constants.projectActions;
  }

  getTasks(): Observable<Task[]> {
    this.saveUserLogued();
    const userRol = this.sessionService.getToken().split('-')[0];

    if (userRol === this.rolTester) {
      return this.testerController.getTask(this.user);
    } else if (userRol === this.rolDeveloper) {
      return this.developerController.getTask(this.user);
    }
    return this.taskService.getTasks();
  }

  updateBug(bugToUpdate: BugState): Observable<any> {
    this.saveUserLogued();
    return this.developerController.updateBug(this.user.id, bugToUpdate);
  }

}
