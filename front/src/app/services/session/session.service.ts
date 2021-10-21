import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Login } from '../../models/session/Login';
import { Token } from '../../models/session/Token';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/sessions`;

  postLogin(login: Login): Observable<Token>{
    return this.http.post<Token>(this.uri + '/login', {
      email: login.Email,
      password: login.Password
    });
  }

  postLogout(){
    return this.http.post(this.uri + '/logout', {
      token: this.getToken()
    });
  }

  saveToken(token: string): void {
    localStorage.setItem('Token', token);
  }

  getToken(): string {
    return localStorage.getItem('Token')!;
  }

  removeToken(): void {
    localStorage.removeItem('Token')
  }

  isUserLogged(): boolean {
    return this.getToken() != null;
  }
  
}
