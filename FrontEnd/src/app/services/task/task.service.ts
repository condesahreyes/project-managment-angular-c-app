import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from 'src/app/models/task/task';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/tasks`;

  createTask(task: Task) {
    return this.http.post<Task>(this.uri, {
      Name: task.Name,
      Project: task.Project,
      Cost: task.Cost,
      Duration: task.Duration
    });
  }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.uri);
  }

  getTasksByProject(idProject : string): Observable<Task[]> {
    return this.http.get<Task[]>(this.uri+'/'+idProject);
  }
}
