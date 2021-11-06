import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Project } from 'src/app/models/project/Project';
import { ProjectService } from 'src/app/services/project/project.service';

@Component({
  selector: 'app-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.css']
})
export class ProjectFormComponent implements OnInit {

  edit: boolean = false;
  form: FormGroup;
  errorMesage: string = "";


  constructor(
    private projectService: ProjectService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<ProjectFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any 

  ) {
    this.form = this.fb.group({
      name: [this.data.name, Validators.required],
    })
  }

  project: Project = {
    Name: ""
  }

  ngOnInit(): void {
    if(this.data !== ""){
      this.edit = true;
    }
  }

  create() {
    this.project.Name = this.form.value.name;
    return this.projectService.createProject(this.project).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  close() {
    this.dialogRef.close(true);
  }

  update(){
    this.project.Name = this.form.value.name;
    return this.projectService.updateProject(this.data.id, this.project).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'error', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }
}