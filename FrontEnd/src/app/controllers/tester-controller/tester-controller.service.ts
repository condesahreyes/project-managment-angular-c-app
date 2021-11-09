import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { UserEntryModel } from 'src/app/models/users/UserEntryModel';
import { TesterService } from 'src/app/services/tester/tester.service';

@Injectable({
  providedIn: 'root'
})
export class TesterControllerService {

  constructor(private testerService: TesterService) { }

  getBugs(user: UserEntryModel) : Observable<Bug[]>{
    return this.testerService.getAllBugsByTester(user.id);
  }

  getProjects(user: UserEntryModel) : Observable<ProjectOut[]>{
    return this.testerService.getAllProjectsByTester(user.id);
  }

}
