import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import { Bug } from 'src/app/models/bug/Bug';
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
      project: [this.data.project, Validators.required]
    })
  }

  bug: Bug = {
    Project: "",
    Id: 0,
    Name: "",
    Domain: "",
    Version: "",
    State: "",
    CreatedBy: "",
  }

  ngOnInit(): void {
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
     this.sessionService.getUserIdLogged().subscribe(u => {
      this.userId = u.id;
    });
    this.bug.Name = this.form.value.name;
    this.bug.Id = this.form.value.id;
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
    // this.project.Name = this.form.value.name;
    // return this.projectService.updateProject(this.data.id, this.project).subscribe(() => {
    //   this.close();
    // });
  }

  close() {
    this.dialogRef.close(true)
  }
}
