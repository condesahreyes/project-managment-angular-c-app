import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../models/users/User';
import { UserService } from 'src/app/services/user/user.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { UserFormComponent } from './user-form/user-form.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit, AfterViewInit {


  displayedColumns = ['name', 'lastName', 'email', 'rol'];
  users: User[] = [];
  dataSource!: MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private userService: UserService,
    public dialog: MatDialog
  ) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getUsersCreated();
    console.log(this.dataSource);

  }

  getUsersCreated() {
    this.userService.getUsers().subscribe(u => {
      this.users = u
    });
    this.dataSource = new MatTableDataSource(this.users);
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
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

}
