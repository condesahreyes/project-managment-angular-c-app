import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { UsersControllerService } from 'src/app/controllers/users-controller.service';
import { Task } from 'src/app/models/task/task';
import { ProjectService } from 'src/app/services/project/project.service';
import { TaskService } from 'src/app/services/task/task.service';
import { TaskFormComponent } from './task-form/task-form.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  displayedColumns = ['name', 'project', 'duration', 'cost'];
  tasks: Task[] = [];
  dataSource!: MatTableDataSource<Task>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private taskService: TaskService,
    private projectService: ProjectService,
    public dialog: MatDialog,
    private router: ActivatedRoute,
    private userController: UsersControllerService
  ) { }

  project: string = "";
  projectId: any;

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getProject();
    this.getTaskCreated();
  }

  getProject() {
    this.projectId = this.router.snapshot.paramMap.get('id');
    if (this.projectId !== null) {
      this.projectService.getProject(this.projectId).subscribe(p => {
        this.project = p.name;
      });
    }
  }

  getTaskCreated() {
    if (this.projectId == null)
      this.userController.getTasks().subscribe(t => {
        this.tasks = t;
        this.dataSource = new MatTableDataSource(this.tasks);
        this.setPaginatorAndSort();
      });
    else
      this.taskService.getTasksByProject(this.projectId).subscribe(t => {
        this.tasks = t;
        this.dataSource = new MatTableDataSource(this.tasks);
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
    const dialogRef = this.dialog.open(TaskFormComponent, {
      width: '50%',
      data: this.project
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getTaskCreated();
    });
  }
}