import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Task } from 'src/app/models/task/task';
import { TaskService } from 'src/app/services/task/task.service';
import { TaskFormComponent } from './task-form/task-form.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  
  displayedColumns = ['name', 'duration', 'cost', 'actions' ];
  tasks: Task[] = [];
  dataSource!: MatTableDataSource<Task>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private taskService: TaskService,
    public dialog: MatDialog
  ) { }

  setPaginatorAndSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.getTaskCreated();

  }

  getTaskCreated() {
    this.taskService.getTasks().subscribe(t => {
      this.tasks = t
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
      data: ""
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }


}
