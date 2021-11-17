import {  Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../models/users/User';
import { UserService } from 'src/app/services/user/user.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { UserFormComponent } from './user-form/user-form.component';
import { BugsResolvedFormComponent } from './bugs-resolved-form/bugs-resolved-form.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit {

  displayedColumns = ['userName', 'name', 'lastName', 'email', 'rol','price', 'actions'];
  users: User[] = [];
  dataSource!: MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private userService: UserService,
    public dialog: MatDialog
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getUsersCreated();
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

  openForm() {
    const dialogRef = this.dialog.open(UserFormComponent, {
      width: '50%',
      height: '80%'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getUsersCreated();
    });
  }

  openBugsForm(user: any) {
    const dialogRef = this.dialog.open(BugsResolvedFormComponent, {
      width: '25%',
      data: user
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

}
