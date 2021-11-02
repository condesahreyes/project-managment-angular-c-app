import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/models/users/User';
import { DeveloperService } from 'src/app/services/developer/developer.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { TesterService } from 'src/app/services/tester/tester.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-project-assign-form',
  templateUrl: './project-assign-form.component.html',
  styleUrls: ['./project-assign-form.component.css']
})
export class ProjectAssignFormComponent implements OnInit {


  displayedColumns = ['select', 'name', 'lastName', 'email', 'rol'];
  users: User[] = [];
  usersProject: User[] = [];
  dataSource!: MatTableDataSource<User>;
  selection = new SelectionModel<User>(false, []);
  projectToAsign = this.data;
  userSelected: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private projectService: ProjectService,
    private developerService: DeveloperService,
    private testerService: TesterService,
    public dialogRef: MatDialogRef<ProjectAssignFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getUsersCreated();
    this.usersInProject();

  }

  getUsersCreated() {
    this.userService.getUsers().subscribe(u => {
      this.users = u
      this.dataSource = new MatTableDataSource(this.users);
      this.setPaginatorAndSort();

    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  close() {
    this.dialogRef.close(true);
  }

  assignUser() {
    this.userSelected = this.selection.selected[0];
    if (this.userSelected.rol === 'Desarrollador') {
      this.developerService.assignDeveloperToProject(this.projectToAsign.id, this.userSelected.id).subscribe();
    }
    if (this.userSelected.rol === 'Tester') {
      this.testerService.assignTesterToProject(this.projectToAsign.id, this.userSelected.id).subscribe();
    }
    if (this.userSelected.rol === "Administrador") {
      this.error();
    }
    this.close();
  }

  usersInProject() {
    this.projectService.getUsersInOneProject(this.projectToAsign.id).subscribe(u => {
      this.usersProject = u
      // this.usersProject.map(u => 
      //   this.selection.select());
  });
}

error() {
  this.snackBar.open('You can not assign administrator to one project', '', {
    duration: 5000,
    horizontalPosition: 'center',
    verticalPosition: 'bottom'
  });
}

}
