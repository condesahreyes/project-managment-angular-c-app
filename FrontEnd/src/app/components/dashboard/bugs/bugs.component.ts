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
import { ProjectService } from 'src/app/services/project/project.service';
import { TesterService } from 'src/app/services/tester/tester.service';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { User } from 'src/app/models/users/User';
import { UserEntryModel } from 'src/app/models/users/UserEntryModel';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.css']
})
export class BugsComponent implements OnInit {

  bugToDelete: any;
  displayedColumns = ['name', 'domain', 'version', 'state', 'duration', 'actions'];
  bugs: Bug[] = [];
  dataSource!: MatTableDataSource<Bug>;
  user!: UserEntryModel;
  project!: ProjectOut;
  rol: string = "";

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private bugService: BugService,
    private projectService: ProjectService,
    private sessionService: SessionService,
    public dialog: MatDialog,
    private testerService: TesterService,
    private router: ActivatedRoute
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit() {
    this.getTokenLogged();
    this.getProject();
    this.getBugs();
  }

  getTokenLogged() {
    let tokenUserLogged = this.sessionService.getToken();
    this.rol = tokenUserLogged.split("-")[0];
  }

  getProject() {
    const projectId = this.router.snapshot.paramMap.get('id');
    if (projectId !== null) {
      this.projectService.getProject(projectId).subscribe(p => {
        this.project = p;
      });
    }

  }

  getBugs() {
    if (this.project === null) {
      this.getBugsByUser();
    } else {
      this.getBugsByProject();
    }
  }

  getBugsByUser() {
    if (this.rol === "Tester")
      this.getTester();
    else
      this.getAllBugs();
  }

  getBugsByProject() {
    this.projectService.getBugsByProject(this.project.id).subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    });
  }

  getAllBugs() {
    this.bugService.getBugs().subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    });
  }

  getTester() {
    this.sessionService.getUserLogged().subscribe(u => {
      this.user = u
      this.getBugsTester(u.id + "");
    });
  }

  getBugsTester(testerId: string) {
    this.testerService.getAllBugsByTester(testerId).subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    });
  }

  openForm() {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: { project: this.project, bug: "", userRol: this.rol }
    });
    dialogRef.afterClosed().subscribe();
  }

  update(bugToUpdate: any) {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: { bug: bugToUpdate }
    });
    dialogRef.afterClosed().subscribe();
  }

  delete(idBug: number) {
    if (confirm("Are you sure to delete?")) {
      this.bugService.deleteBug(idBug, this.user.id).subscribe();
    }
  }

}