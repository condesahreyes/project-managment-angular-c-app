import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/users/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TesterService {

  constructor(
    private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/testers`;


  assignTesterToProject(projectId: string, testerId: string){
    return this.http.post((this.uri + '/' + testerId + '/' + 'project' + '/' +  projectId), {
    });
  }

  deassignTesterToProject(projectId: string, testerId: string){
    return this.http.delete((this.uri + '/' + testerId + '/' + 'project' + '/' +  projectId), {
    });
  }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.uri).pipe();
  }
}
