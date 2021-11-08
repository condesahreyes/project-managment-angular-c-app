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
  projectId: any;
  tokenUserLogged: string = "";
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

  async ngOnInit(): Promise<void> {
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.user = { userId: u.userId }
    });
    await this.getProject();
    this.getTokenLogged();
    if (this.rol === "Tester"/*this.tokenUserLogged.includes("Tester", 0)*/) {
      this.getBugsTester();
      // this.sessionService.getUserIdLogged().subscribe(u => {
      //   this.user = { userId: u.userId }
      //   this.getBugsTester();
      // });
    }else{
      this.getBugsBySelectedProject();
    }
  }

  getTokenLogged() {
    this.tokenUserLogged = this.sessionService.getToken();
    this.rol = this.tokenUserLogged.split("-")[0];
  }

  getBugsTester() {
    this.testerService.getAllBugsByTesterInProject(this.user.userId, this.projectId).subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    });
  }

  getProject() {
    this.projectId = this.router.snapshot.paramMap.get('id');
    this.projectService.getProject(this.projectId).subscribe(p => {
      this.project = p.name;
    });
  }

  getBugsBySelectedProject() {
    this.projectService.getBugsByProject(this.projectId).subscribe(b => {
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
    this.rol != "Tester" ? this.getBugsBySelectedProject() : this.getBugsTester();
    });
  }

  update(bug: any) {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: bug
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.rol != "Tester" ? this.getBugsBySelectedProject() : this.getBugsTester();
    });
  }

  delete(idBug: number) {
    if (confirm("Are you sure to delete?")) {
      this.bugService.deleteBug(idBug, this.user).subscribe(() => 
      this.rol != "Tester" ? this.getBugsBySelectedProject() : this.getBugsTester()
      );
    }
  }

}
