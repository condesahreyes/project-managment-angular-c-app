import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BugService {

  constructor(private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/bugs`;

  createBug(bug: Bug) {
    return this.http.post<Bug>(this.uri, {
      Project: bug.Project,
      Id: bug.Id,
      Name: bug.Name,
      Domain: bug.Domain,
      Version: bug.Version,
      State: bug.State,
      CreatedBy: bug.CreatedBy
    });
  }

  // getProject(idProject : string): Observable<ProjectOut> {
  //   return this.http.get<ProjectOut>(this.uri + '/' + idProject, {

  //   });
  // }

  getBug(idBug : string): Observable<Bug> {
    return this.http.get<Bug>(this.uri + '/' + idBug, {

    });
  }

  getBugs(): Observable<Bug[]> {
    return this.http.get<Bug[]>(this.uri, {

    });
  }

  deleteBug(idBug : string, idUser: string): Observable<Bug> {
    return this.http.delete<Bug>(this.uri + '/' + idBug, {
      
    });
  }
}
