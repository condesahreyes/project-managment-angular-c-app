import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'src/app/models/users/User';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  form: FormGroup;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<UserFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) { 
    this.form = this.fb.group({
      name: ["", Validators.required],
      lastName: ["", Validators.required],
      userName: ["", Validators.required],
      rol: ["", Validators.required],
      email: ["", Validators.required],
      password: ["", Validators.required]
    })
  }

  user: User = {
    Rol: "",
    Name: "",
    LastName: "",
    UserName: "",
    Password: "",
    Email: "",
  }


  ngOnInit(): void {
  }

  create() {
    this.user.Name = this.form.value.name;
    this.user.LastName = this.form.value.lastName;
    this.user.UserName = this.form.value.userName;
    this.user.Email = this.form.value.email;
    this.user.Password = this.form.value.password;
    this.user.Rol = this.form.value.rol;

    return this.userService.createUser(this.user).subscribe();
  }

  close(){
    this.dialogRef.close
  }
}
