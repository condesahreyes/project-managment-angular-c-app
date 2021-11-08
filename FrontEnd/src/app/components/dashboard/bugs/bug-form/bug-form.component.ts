import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Bug } from 'src/app/models/bug/Bug';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { BugService } from 'src/app/services/bug/bug.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { SessionService } from 'src/app/services/session/session.service';
import { TesterService } from 'src/app/services/tester/tester.service';

@Component({
  selector: 'app-bug-form',
  templateUrl: './bug-form.component.html',
  styleUrls: ['./bug-form.component.css']
})
export class BugFormComponent implements OnInit {

  edit: boolean = false;
  form: FormGroup;
  projects: ProjectOut[] = [];
  userId: string = "";
  errorMesage: string = "";

  constructor(
    private bugService: BugService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private sessionService: SessionService,
    public dialogRef: MatDialogRef<BugFormComponent>,
    private testerService: TesterService,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: [this.data.bug.name, Validators.required],
      domain: [this.data.bug.domain, Validators.required],
      version: [this.data.bug.version, Validators.required],
      id: [this.data.bug.id, Validators.required],
      state: [this.data.bug.state, Validators.required],
      project: [this.data.project, Validators.required],
      duration: [this.data.bug.duration, Validators.required],

    })

    if(this.data.project == null){
      this.form.addControl('project', new FormControl([this.data.project, Validators.required]))
    }
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
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.userId = u.userId + "";
    });
    this.getProjects();
    if (this.data.domain) {
      this.edit = true;
    }
  }

  getProjects(){
    if(this.data.rol == "Tester")
      this.getTester();
    else 
      this.getAllProjects();
  }

  getTester() {
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.getProjectTester(u.userId);
    });
  }

  getProjectTester(userId : any) {
    this.testerService.getAllProjectsByTester(userId).subscribe(p => {
      this.projects = p;
    });
  }


  getAllProjects() {
    this.projectService.getProjects().subscribe(p => {
      this.projects = p
    });
  }

  create() {
    this.bug.Name = this.form.value.name;
    this.bug.Id = this.form.value.id;
    this.bug.Duration = this.form.value.duration;
    this.bug.Domain = this.form.value.domain;
    this.bug.Version = this.form.value.version;
    this.bug.State = this.form.value.state;
    this.bug.Project = this.data;
    this.bug.CreatedBy = this.userId;

    return this.bugService.createBug(this.bug).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  update() {
    this.bugUpdate.Project = this.data.project;
    this.bugUpdate.Name = this.form.value.name;
    this.bugUpdate.Domain = this.form.value.domain;
    this.bugUpdate.Version = this.form.value.version;
    this.bugUpdate.State = this.form.value.state;
    this.bugUpdate.UserId = this.userId;
    this.bugUpdate.Duration = this.form.value.duration;

    return this.bugService.updateBug(this.data.id, this.bugUpdate).subscribe(() => {
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
