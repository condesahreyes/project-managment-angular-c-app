import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Bug } from 'src/app/models/bug/Bug';
import { BugService } from 'src/app/services/bug/bug.service';
import { BugFormComponent } from './bug-form/bug-form.component';
import { SessionService } from 'src/app/services/session/session.service';
import { UserIdModel } from 'src/app/models/users/UserIdModel';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.css']
})
export class BugsComponent implements OnInit {

  bugToDelete: any;
  displayedColumns = ['name', 'domain', 'version', 'state', 'duration', 'actions']; //'project', 'solved by',
  bugs: Bug[] = [];
  dataSource!: MatTableDataSource<Bug>;
  user!: UserIdModel;
  project: any

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private bugService: BugService,
    private sessionService: SessionService,
    public dialog: MatDialog,
    private router: ActivatedRoute
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getProject();
    // this.getBugsCreated();
    this.getBugsBySelectedProject();
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.user = { userId:  u.userId}
    });
  
  }

  // getBugsCreated() {
  //   this.bugService.getBugs().subscribe(b => {
  //     this.bugs = b
  //     this.dataSource = new MatTableDataSource(this.bugs);
  //     this.setPaginatorAndSort();
  //   });
  // }

  getProject(){
    this.router.queryParams.subscribe((params: any) =>{
      this.project = params.data
    })
  }
  
  getBugsBySelectedProject(){
    this.bugService.getBugsByProject(this.project).subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
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
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: this.project
    });
    dialogRef.afterClosed().subscribe(() => {
      // this.getBugsCreated();
      this.getBugsBySelectedProject();
    });
  }

  update(bug: any) {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: bug
    });
    dialogRef.afterClosed().subscribe((result) => {
      // this.getBugsCreated();
      this.getBugsBySelectedProject();
    });
  }

  delete(idBug: number) {
    if (confirm("Are you sure to delete?")) {

      this.bugService.deleteBug(idBug, this.user).subscribe(() =>this.getBugsBySelectedProject()/*this.getBugsCreated()*/);
    }
  }

}
