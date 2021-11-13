import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsersControllerService } from 'src/app/controllers/users-controller.service';
import { Bug } from 'src/app/models/bug/Bug';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { BugService } from 'src/app/services/bug/bug.service';

@Component({
  selector: 'app-bug-form',
  templateUrl: './bug-form.component.html',
  styleUrls: ['./bug-form.component.css']
})
export class BugFormComponent implements OnInit {

  edit: boolean = false;
  form: FormGroup;
  projects: ProjectOut[] = [];
  errorMesage: string = "";

  constructor(
    private bugService: BugService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<BugFormComponent>,
    private userController: UsersControllerService,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: [this.data.bug.name, Validators.required],
      domain: [this.data.bug.domain, Validators.required],
      version: [this.data.bug.version, Validators.required],
      id: [this.data.bug.id, Validators.required],
      state: [this.data.bug.state, Validators.required],
      project: [this.data.bug.project, this.data.project === undefined ? Validators.required : false ],
      duration: [this.data.bug.duration, Validators.required]
    })

  }

  bugUpdate: BugUpdate = {
    Project: "",
    Name: "",
    Domain: "",
    Version: "",
    State: "",
    UserId: "",
    Duration: 0
  }

  bug: Bug = {
    Project: "",
    Id: 0,
    Name: "",
    Domain: "",
    Version: "",
    State: "",
    CreatedBy: "",
    Duration: 0
  }

  ngOnInit(): void {
    this.getProjects();

    if (this.data.bug !== "") {
      this.edit = true;
    }
  }

  getProjects(){
    this.userController.getProjects().subscribe(p => this.projects = p);
  }

  create() {
    this.bug.Name = this.form.value.name;
    this.bug.Id = this.form.value.id;
    this.bug.Duration = this.form.value.duration;
    this.bug.Domain = this.form.value.domain;
    this.bug.Version = this.form.value.version;
    this.bug.State = this.form.value.state;
    this.bug.Project = (this.data.project === undefined) ? this.form.value.project : this.data.project.name;
    this.bug.CreatedBy = this.userController.getUserLogued().id;

    return this.bugService.createBug(this.bug).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  update() {
    this.bugUpdate.Project =  this.form.value.project;
    this.bugUpdate.Name = this.form.value.name;
    this.bugUpdate.Domain = this.form.value.domain;
    this.bugUpdate.Version = this.form.value.version;
    this.bugUpdate.State = this.form.value.state;
    this.bugUpdate.UserId = this.userController.getUserLogued().id;
    this.bugUpdate.Duration = this.form.value.duration;

    return this.bugService.updateBug(this.data.bug.id, this.bugUpdate).subscribe(() => {
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
