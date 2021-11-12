import { UsersControllerService } from 'src/app/controllers/users-controller.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { BugFormComponent } from './bug-form/bug-form.component';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { BugService } from 'src/app/services/bug/bug.service';
import { MatTableDataSource } from '@angular/material/table';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { Bug } from 'src/app/models/bug/Bug';
import { BugDesaFormComponent } from './bug-desa-form/bug-desa-form.component';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.css']
})
export class BugsComponent implements OnInit {

  dataSource!: MatTableDataSource<Bug>;
  displayedColumns : any;
  actions: string[] = [];

  project!: ProjectOut;
  bugToDelete: any;
  bugs: Bug[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private bugService: BugService,
    private projectService: ProjectService,
    public dialog: MatDialog,
    private router: ActivatedRoute,
    private userController: UsersControllerService
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
    this.getColumns();
    this.getActions();
    this.getBugs();
  }

  getColumns(){
    this.displayedColumns = this.userController.getBugsColumns();
  }

  getActions(){
    this.actions = this.userController.getActionsBugs();
  }

  getBugs() {
    const projectId = this.router.snapshot.paramMap.get('id');

    if (projectId !== null) {
      this.projectService.getProject(projectId).subscribe(p => {
        this.project = p;
        this.getBugsByProject();
      });
    }else{
      this.getBugsByUser();
    }
  }

  getBugsByProject() {
    this.projectService.getBugsByProject(this.project.id).subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    });
  }

  getBugsByUser() {
    this.userController.getBugs().subscribe(b => {
      this.bugs = b
      this.dataSource = new MatTableDataSource(this.bugs);
      this.setPaginatorAndSort();
    })
  }

  openForm() {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: { project: this.project, bug: "" }
    });
    dialogRef.afterClosed().subscribe(() => { 
      this.getColumns();
      this.getBugs();
    });
  }

  update(bugToUpdate: any) {
    const dialogRef = this.dialog.open(BugFormComponent, {
      width: '50%',
      data: { bug: bugToUpdate }
    });
    dialogRef.afterClosed().subscribe();
  }

  updateState(bugToUpdate: any) {
    const dialogRef = this.dialog.open(BugDesaFormComponent, {
      width: '50%',
      data: { bug: bugToUpdate }
    });
    dialogRef.afterClosed().subscribe(() => { 
      this.getColumns();
      this.getBugs();
    });
  }

  delete(idBug: number) {
    if (confirm("Are you sure to delete?")) {
      const userId = this.userController.getUserLogued().id; 
      this.bugService.deleteBug(idBug, userId).subscribe(() => {
        this.getColumns();
        this.getBugs();
      });
    }
  }

}