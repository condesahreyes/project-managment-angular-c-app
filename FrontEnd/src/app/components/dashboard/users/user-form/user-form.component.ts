import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from 'src/app/models/users/User';
import { SessionService } from 'src/app/services/session/session.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  form: FormGroup;
  errorMesage: string = "";
  usersRolPrice = ["desarrollador", "tester"];

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<UserFormComponent>,
    public sessionService : SessionService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = this.fb.group({
      name: ["", Validators.required],
      lastName: ["", Validators.required],
      userName: ["", Validators.required],
      rol: ["", Validators.required],
      email: ["", Validators.required],
      password: ["", Validators.required],
      price: [""]
    })
  }

  user: User = {
    rol: "",
    name: "",
    lastName: "",
    userName: "",
    password: "",
    email: "",
    price: 0
  }

  ngOnInit(): void { }

  create() {
    this.user.name = this.form.value.name;
    this.user.lastName = this.form.value.lastName;
    this.user.userName = this.form.value.userName;
    this.user.email = this.form.value.email;
    this.user.password = this.form.value.password;
    this.user.rol = this.form.value.rol;
    this.user.price = (this.usersRolPrice.includes(this.form.value.rol.toLowerCase())) ? this.form.value.price : 0;

    return this.userService.createUser(this.user).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  close() {
    this.dialogRef.close(true)
  }

  error(message: string) {
    this.snackBar.open(message, 'error', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }
}