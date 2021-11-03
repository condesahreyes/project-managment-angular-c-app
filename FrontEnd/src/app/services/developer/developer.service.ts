import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/users/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  constructor(
    private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/developers`;


  assignDeveloperToProject(projectId: string, developerId: string){
    return this.http.post((this.uri + '/' + developerId + '/' + 'project' + '/' +  projectId), {
    });
  }

  deassignDeveloperToProject(projectId: string, developerId: string){
    return this.http.delete((this.uri + '/' + developerId + '/' + 'project' + '/' +  projectId), {
    });
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.uri).pipe();
  }

}
