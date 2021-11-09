import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { UserEntryModel } from 'src/app/models/users/UserEntryModel';
import { DeveloperService } from 'src/app/services/developer/developer.service';

@Injectable({
  providedIn: 'root'
})
export class DeveloperControllerService {

  constructor(private developerService : DeveloperService) { }

  getBugs(user: UserEntryModel) : Observable<Bug[]>{
    return this.developerService.getAllBugsByDeveloper(user.id);
  }

  getProjects(user: UserEntryModel) : Observable<ProjectOut[]>{
    return this.developerService.getAllProjectsByDeveloper(user.id);
  }
}
