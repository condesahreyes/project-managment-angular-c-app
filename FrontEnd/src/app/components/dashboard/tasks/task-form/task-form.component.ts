import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { Task } from 'src/app/models/task/task';
import { ProjectService } from 'src/app/services/project/project.service';
import { TaskService } from 'src/app/services/task/task.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  edit: boolean = false;
  form: FormGroup;
  projects: ProjectOut[] = [];
  userId: string = "";
  
  constructor(
    private taskService: TaskService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<TaskFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: [this.data.name, Validators.required],
      duration: [this.data.duration, Validators.required],
      cost: [this.data.cost, Validators.required],
      project: [this.data.project, Validators.required],
   
    })
  }

  // bugUpdate: BugUpdate = {
  //   Project: "",
  //   Name: "",
  //   Domain: "",
  //   Version: "",
  //   State: "",
  //   UserId: "",
  //   Duration: 0
  // }

  task: Task = {
    Name: "",
    Cost: 0,
    Duration: 0,
    Project: ""
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
    this.task.Name = this.form.value.name;
    this.task.Duration = this.form.value.duration;
    this.task.Cost = this.form.value.cost;
    this.task.Project = this.form.value.project;

    return this.taskService.createTask(this.task).subscribe(() => {
      this.close();
    });
  }

  update(){
    // this.bugUpdate.Project = this.form.value.project;
    // this.bugUpdate.Name = this.form.value.name;
    // this.bugUpdate.Domain = this.form.value.domain;
    // this.bugUpdate.Version = this.form.value.version;
    // this.bugUpdate.State = this.form.value.state;
    // this.bugUpdate.UserId = this.userId;
    // this.bugUpdate.Duration = this.form.value.duration;

    // return this.bugService.updateBug(this.data.id, this.bugUpdate).subscribe(() => this.close());
  }

  close() {
    this.dialogRef.close(true)
  }
}
