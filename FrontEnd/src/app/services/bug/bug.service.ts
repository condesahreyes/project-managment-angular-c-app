import { HttpClient } from '@angular/common/http';
import { Injectable, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { UserIdModel } from 'src/app/models/users/UserIdModel';
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
      CreatedBy: bug.CreatedBy,
      Duration: bug.Duration
    });
  }

  getBug(idBug : string): Observable<Bug> {
    return this.http.get<Bug>(this.uri + '/' + idBug, {
    });
  }

  getBugs(): Observable<Bug[]> {
    return this.http.get<Bug[]>(this.uri, {

    });
  }

  deleteBug(idBug : number, userId: string){
    return this.http.delete<UserIdModel>(this.uri + '/' + idBug + '/byUser/' +userId
    );
  }

  updateBug(idBug : any, bug: BugUpdate): Observable<any> {
    return this.http.put<BugUpdate>(this.uri + '/' + idBug, {
      Project: bug.Project,
      Name: bug.Name,
      Domain: bug.Domain,
      Version: bug.Version,
      State: bug.State,
      UserId: bug.UserId,
      Duration: bug.Duration
    });
  }
}
