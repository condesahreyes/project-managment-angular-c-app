import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { BugState } from 'src/app/models/bug/BugState';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { Task } from 'src/app/models/task/task';
import { User } from 'src/app/models/users/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  constructor(
    private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/developers`;

  assignDeveloperToProject(projectId: string, developerId: string) {
    return this.http.post((this.uri + '/' + developerId + '/' + 'project' + '/' + projectId), {
    });
  }

  deassignDeveloperToProject(projectId: string, developerId: string) {
    return this.http.delete((this.uri + '/' + developerId + '/' + 'project' + '/' + projectId), {
    });
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.uri).pipe();
  }

  getAllBugsByDeveloper(developerId: string): Observable<Bug[]> {
    return this.http.get<Bug[]>(this.uri + '/' + developerId + '/bugs');
  }

  getAllProjectsByDeveloper(developerId: string): Observable<ProjectOut[]> {
    return this.http.get<ProjectOut[]>(this.uri + '/' + developerId + '/projects');
  }

  getTasks(idDeveloper: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.uri + '/' + idDeveloper + '/tasks');
  }

  getCountBugsResolvedByDeveloper(idDeveloper: string): Observable<number> {
    return this.http.get<number>(this.uri + '/' + idDeveloper + '/countBugs');
  }

  updateStateBug(developerId: string, bugToUpdate: BugState): Observable<any> {
    return this.http.put<BugUpdate>(this.uri + '/' + developerId + '/bugState', {
      State: bugToUpdate.State,
      BugId: bugToUpdate.Id
    });
  }
}
