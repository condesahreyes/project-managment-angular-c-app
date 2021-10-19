import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/services/session.service';
import { Login } from 'src/models/session/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  loading = false;


  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private sessionService: SessionService,
    private router: Router) {

    this.form = this.fb.group({
      email: ["", Validators.required],
      password: ["", Validators.required]
    })

  }

  login: Login = {
    Email: "",
    Password: ""
  }

  ngOnInit(): void {

  }

  onLogin() {
    this.login.Email = this.form.value.email;
    this.login.Password = this.form.value.password;

    return this.sessionService.postLogin(this.login).subscribe(data => {
      this.sessionService.saveToken(data.token);
      this.loadUser();
      //this.router.navigate(['dashboard']);
    }, error => {
      this.error();
      this.form.reset();
    });
  }
  
  onLogout() {
    return this.sessionService.postLogout().subscribe(() => {
      this.router.navigate(['login'])
      this.sessionService.removeToken();
    });
  }

  error() {
    this.snackBar.open('User or Password are invalid', '', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }

  loadUser() {
    this.loading = true;
    setTimeout(() => {
      this.router.navigate(['dashboard']);
    }, 1500);
  }
  // loginTo() {
  //   const email = this.form.value.user;
  //   const password = this.form.value.password;

  //   if (email == "diegoasa" && password == "123456") {
  //     this.loadUser();
  //   } else {
  //     this.error();
  //     this.form.reset();
  //   }
  // }


}
