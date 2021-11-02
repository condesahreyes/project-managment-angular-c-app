import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Guid } from 'guid-typescript';
import { Bug } from 'src/app/models/bug/Bug';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { BugService } from 'src/app/services/bug/bug.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { SessionService } from 'src/app/services/session/session.service';

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
  
  constructor(
    private bugService: BugService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    private sessionService: SessionService,
    public dialogRef: MatDialogRef<BugFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: [this.data.name, Validators.required],
      domain: [this.data.domain, Validators.required],
      version: [this.data.version, Validators.required],
      id: [this.data.id, Validators.required],
      state: [this.data.state, Validators.required],
      project: [this.data.project, Validators.required],
      duration: [this.data.duration, Validators.required]
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
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.userId = u.userId + "";
    });
    this.getProjectsCreated();
    if(this.data !== ""){
      this.edit = true;
    }
  }

    getProjectsCreated() {
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
    this.bug.Project = this.form.value.project;
    this.bug.CreatedBy = this.userId;

    
    return this.bugService.createBug(this.bug).subscribe(() => {
      this.close();
    });
  }

  update(){
    this.bugUpdate.Project = this.form.value.project;
    this.bugUpdate.Name = this.form.value.name;
    this.bugUpdate.Domain = this.form.value.domain;
    this.bugUpdate.Version = this.form.value.version;
    this.bugUpdate.State = this.form.value.state;
    this.bugUpdate.UserId = this.userId;
    this.bugUpdate.Duration = this.form.value.duration;

    return this.bugService.updateBug(this.data.id, this.bugUpdate).subscribe(() => this.close());
  }

  close() {
    this.dialogRef.close(true)
  }
}
