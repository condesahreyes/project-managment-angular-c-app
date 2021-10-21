import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/users/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient) { }
  
  private uri: string = `${environment.URI_BASE}/users`;

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.uri).pipe();
  }

}
