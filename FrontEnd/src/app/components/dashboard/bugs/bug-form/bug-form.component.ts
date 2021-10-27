import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Guid } from 'guid-typescript';
import { Bug } from 'src/app/models/bug/Bug';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { BugService } from 'src/app/services/bug/bug.service';
import { ProjectService } from 'src/app/services/project/project.service';

@Component({
  selector: 'app-bug-form',
  templateUrl: './bug-form.component.html',
  styleUrls: ['./bug-form.component.css']
})
export class BugFormComponent implements OnInit {

  form: FormGroup;
  projects: ProjectOut[] = [];
  projectToAsign: string = "";

  pepe: string = "diego"
  constructor(
    private bugService: BugService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<BugFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: ["", Validators.required],
      domain: ["", Validators.required],
      version: ["", Validators.required],
      id: ["", Validators.required],
      state: ["", Validators.required],
    })
  }


  bug: Bug = {
    Project: "",
    Id: 0,
    Name: "",
    Domain: "",
    Version: "",
    State: "",
    //CreatedBy: "",
  }


  ngOnInit(): void {
    this.getProjectsCreated();
  }

  getProjectsCreated() {
    this.projectService.getProjects().subscribe(p => {
      console.log(p);

      this.projects = p
    });
  }
  create() {
    this.bug.Name = this.form.value.name;
    this.bug.Id = this.form.value.id;
    this.bug.Domain = this.form.value.domain;
    this.bug.Version = this.form.value.version;
    this.bug.State = this.form.value.state;
    this.bug.Project = this.form.value.project.name;

    return this.bugService.createBug(this.bug).subscribe();
  }

  close() {
    this.dialogRef.close
  }
}
