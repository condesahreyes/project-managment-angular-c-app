import { ProjectAssignFormComponent } from './project-assign-form/project-assign-form.component';
import { UsersControllerService } from 'src/app/controllers/users-controller.service';
import { ProjectFormComponent } from './project-form/project-form.component';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { MatTableDataSource } from '@angular/material/table';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  displayedColumns: any = [];
  actions: string[] = [];
  projects: ProjectOut[] = [];
  dataSource!: MatTableDataSource<ProjectOut>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private projectService: ProjectService,
    public dialog: MatDialog,
    private router: Router,
    private userController: UsersControllerService
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(){
    this.getProjects();
    this.getActions();
  }

  getProjects(){
    this.userController.getProjects().subscribe(p => {
      this.displayedColumns = this.userController.getProjectColumns();
      this.dataSource = new MatTableDataSource(p);
      this.projects = p
      this.setPaginatorAndSort();
    });
  }

  getActions(){
    this.actions = this.userController.getActionsProject();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openForm() {
    const dialogRef = this.dialog.open(ProjectFormComponent, {
      width: '50%',
      data: ""
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getProjects();
    });
  }

  delete(idProject: any) {
    if (confirm("Are you sure to delete?")) {
      this.projectService.deleteProject(idProject).subscribe(data => {
        this.getProjects();
      });
    }
  }

  update(project: any) {
    const dialogRef = this.dialog.open(ProjectFormComponent, {
      width: '50%',
      data: project
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getProjects();
    });
  }

  seeProject(projectId: string) {
    this.router.navigateByUrl('dashboard/projects' + '/' + projectId);
  }

  assignUser(project: any) {
    const dialogRef = this.dialog.open(ProjectAssignFormComponent, {
      width: '50%',
      data: project
    });
    dialogRef.afterClosed().subscribe();
  }
}