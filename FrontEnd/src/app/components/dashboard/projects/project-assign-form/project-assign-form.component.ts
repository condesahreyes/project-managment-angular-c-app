import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/models/users/User';
import { DeveloperService } from 'src/app/services/developer/developer.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { TesterService } from 'src/app/services/tester/tester.service';

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
  selection = new SelectionModel<User>(true, []);
  projectToAsign = this.data;
  userSelected: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private snackBar: MatSnackBar,
    private projectService: ProjectService,
    private developerService: DeveloperService,
    private testerService: TesterService,
    public dialogRef: MatDialogRef<ProjectAssignFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.testerService.getAll().subscribe(t => {
      this.developerService.getAll().subscribe(d => {
        this.users = t.concat(d);
        this.dataSource = new MatTableDataSource<User>(this.users);
        this.setPaginatorAndSort();
        this.loadUsersInProject();
      })
    });
  }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadUsersInProject() {
      this.projectService.getUsersInOneProject(this.projectToAsign.id).subscribe(u => {
        this.usersProject = u;
        this.initialSelectUsers();
    });
  }

  initialSelectUsers(){
    this.dataSource.data.forEach(row => {
      this.usersProject.forEach(u => {
        if(u.email == row.email)
        this.selection.select(row);
        })
      }
    );
  }

  selectUser(user : any) {
    if(this.selection.isSelected(user))
      this.deassignUser(user);
    else
      this.assignUser(user);

    this.selection.toggle(user);
  }

  assignUser(user:any){
    this.selection.select(user);
    let lastSelected = this.selection.selected.length -1;
    this.userSelected = this.selection.selected[lastSelected];

    if (this.userSelected.rol === 'Desarrollador') {
      this.developerService.assignDeveloperToProject(this.projectToAsign.id, this.userSelected.id).subscribe();
    }
    if (this.userSelected.rol === 'Tester') {
      this.testerService.assignTesterToProject(this.projectToAsign.id, this.userSelected.id).subscribe();
    }

  }

  deassignUser(user: any){
    this.selection.deselect(user);

    if (user.rol === 'Desarrollador') {
      this.developerService.deassignDeveloperToProject(this.projectToAsign.id, user.id).subscribe();
    }
    if (user.rol === 'Tester') {
      this.testerService.deassignTesterToProject(this.projectToAsign.id, user.id).subscribe();
    }

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

  error() {
    this.snackBar.open('You can not assign administrator to one project', '', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }

}
