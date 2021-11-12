import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { BugState } from 'src/app/models/bug/BugState';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { Task } from 'src/app/models/task/task';
import { UserEntryModel } from 'src/app/models/users/UserEntryModel';
import { DeveloperService } from 'src/app/services/developer/developer.service';

@Injectable({
  providedIn: 'root'
})
export class DeveloperControllerService {

  constructor(
    private developerService: DeveloperService
    ) { }

  getBugs(user: UserEntryModel): Observable<Bug[]> {
    return this.developerService.getAllBugsByDeveloper(user.id);
  }

  getProjects(user: UserEntryModel): Observable<ProjectOut[]> {
    return this.developerService.getAllProjectsByDeveloper(user.id);
  }

<<<<<<< HEAD
  getTask(user: UserEntryModel) : Observable<Task[]>{
    return this.developerService.getTasks(user.id);
=======
  updateBug(developerId: string, bugToUpdate: BugState): Observable<any> {
    return this.developerService.updateStateBug(developerId, bugToUpdate);
>>>>>>> feature/ajustesRolDeveloper
  }
}
