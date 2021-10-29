import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/models/users/User';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-project-assign-form',
  templateUrl: './project-assign-form.component.html',
  styleUrls: ['./project-assign-form.component.css']
})
export class ProjectAssignFormComponent implements OnInit {

  
  displayedColumns = ['name', 'lastName', 'email', 'rol'];
  users: User[] = [];
  dataSource!: MatTableDataSource<User>;
  selection = new SelectionModel<User>(true, []);
  projectToAsign = this.data;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private userService: UserService,
    public dialogRef: MatDialogRef<ProjectAssignFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any 
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getUsersCreated();
    console.log(this.projectToAsign)

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



}
