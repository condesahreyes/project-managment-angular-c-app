import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import { Injectable } from '@angular/core';
import {Project} from "../../models/project/Project";
import {Observable} from "rxjs";
import {ProjectOut} from "../../models/project/ProjectOut";
import { User } from "src/app/models/users/User";
import { Bug } from "src/app/models/bug/Bug";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/projects`;

  createProject(project : Project) {
    return this.http.post<Project>(this.uri, {
      Name: project.Name
    });
  }

  getProject(idProject : string): Observable<ProjectOut> {
    return this.http.get<ProjectOut>(this.uri + '/' + idProject, {

    });
  }

  getProjects(): Observable<ProjectOut[]> {
    return this.http.get<ProjectOut[]>(this.uri).pipe();
  }

  deleteProject(idProject : string): Observable<ProjectOut> {
    return this.http.delete<ProjectOut>(this.uri + '/' + idProject, {
      
    });
  }

  getBugsByProject(projecId: string): Observable<Bug[]> {
    return this.http.get<Bug[]>(this.uri + '/' + projecId + '/bugs', {

    });
  }

  updateProject(idProject: string, project: Project): Observable<Project> {
    console.log("el id es " + idProject)
    return this.http.put<Project>( this.uri + "/" + idProject, {
      Name: project.Name
    });//.pipe(catchError(this.handlerError.handleError));
  }

  getUsersInOneProject(idProject : string): Observable<User[]> {
    return this.http.get<User[]>(this.uri + '/' + idProject + '/' + 'users', {
    });

  }
}
