import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {Login} from "../../models/session/Login";
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private sessionService: SessionService, private router: Router) { }

  login: Login = {
    Email: "",
    Password: ""
  }

  ngOnInit(): void {
  }

  onLogin() {
    return this.sessionService.postLogin(this.login).subscribe(data => {
      this.router.navigate(['/home']);
    }, error => {
      console.error(error)
    });
  }

}
