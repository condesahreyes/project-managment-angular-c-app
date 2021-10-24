import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import { Injectable } from '@angular/core';
import {Project} from "../../models/project/Project";
import {Observable} from "rxjs";
import {ProjectOut} from "../../models/project/ProjectOut";

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
    return this.http.get<ProjectOut[]>(this.uri, {

    });
  }

  deleteProject(idProject : string): Observable<ProjectOut> {
    return this.http.delete<ProjectOut>(this.uri + '/' + idProject, {
      
    });
  }

  updateProject(idProject: string, project: Project): Observable<Project> {
    return this.http.put<Project>(`${this.uri}/${idProject}`, {
      Name: project.Name
    });//.pipe(catchError(this.handlerError.handleError));
  }
}
