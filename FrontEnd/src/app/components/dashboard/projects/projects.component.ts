import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Project } from 'src/app/models/project/Project';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectAssignFormComponent } from './project-assign-form/project-assign-form.component';
import { ProjectFormComponent } from './project-form/project-form.component';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  displayedColumns = ['name', 'TotalBugs', 'actions'];
  projects: Project[] = [];
  dataSource!: MatTableDataSource<Project>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private projectService: ProjectService,
    public dialog: MatDialog
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getProjectsCreated();

  }

  getProjectsCreated() {
    this.projectService.getProjects().subscribe(p => {
      this.projects = p
      this.dataSource = new MatTableDataSource(this.projects);
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
    const dialogRef = this.dialog.open(ProjectFormComponent, {
      width: '50%',
      data: ""
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  delete(idProject: any) {
    if (confirm("Are you sure to delete?")) {
      this.projectService.deleteProject(idProject).subscribe(data => {
      });
      this.projectService.getProjects().subscribe((response) => {
        this.projects = response;
      })
    }
  }

  update(project: any) {
    const dialogRef = this.dialog.open(ProjectFormComponent, {
      width: '50%',
      data: project
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  assignUser(project: any) {
    const dialogRef = this.dialog.open(ProjectAssignFormComponent, {
      width: '50%',
      data: project
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
