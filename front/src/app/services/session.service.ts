import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Token} from "../models/session/Token";
import {Login} from "../models/session/Login";


@Injectable()
export class SessionService {

  constructor(private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/sessions`;

  postLogin(login: Login){
    return this.http.post<Token>(this.uri + '/login', {
      email: login.Email,
      password: login.Password
    });
  }
}
