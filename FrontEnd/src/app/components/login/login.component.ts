import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/services/session/session.service';
import { Login } from '../../models/session/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  loading = false;
  errorMesage: string = "";


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
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
      this.form.reset();
    });
  }

  onLogout() {
    return this.sessionService.postLogout().subscribe(() => {
      this.router.navigateByUrl('login');
      this.sessionService.removeToken();
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'error', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }

  loadUser() {
    this.loading = true;
    setTimeout(() => {
      this.router.navigateByUrl('dashboard');
    }, 1500);
  }
}
