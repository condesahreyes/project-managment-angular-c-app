import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
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

  form: FormGroup;
  projects: ProjectOut[] = [];
  userId: string = "";
  errorMesage: string = "";
  
  constructor(
    private taskService: TaskService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<TaskFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

  ) {
    this.form = this.fb.group({
      name: ["", Validators.required],
      duration: ["", Validators.required],
      cost: ["", Validators.required]
    })
    if(this.data == "")
    this.form.addControl('project', new FormControl([this.data.project, Validators.required]))
  }

  task: Task = {
    Name: "",
    Cost: 0,
    Duration: 0,
    Project: ""
  }

  ngOnInit(): void {
    this.getProjectsCreated();
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

    this.task.Project = (this.data == "") ? this.form.value.project : this.data;

    return this.taskService.createTask(this.task).subscribe(() => {
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
