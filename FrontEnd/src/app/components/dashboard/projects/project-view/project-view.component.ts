import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { ProjectService } from 'src/app/services/project/project.service';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit {

  constructor(
    private router: ActivatedRoute, 
    private projectService: ProjectService
    ) { }

  project!: ProjectOut;
  projectName: String ="";

  ngOnInit(): void {
    this.getProject();
  }

  getProject(){
    const projectId = this.router.snapshot.paramMap.get('id') +"";
    this.projectService.getProject(projectId).subscribe(p => {
      this.projectName = p.name;
      this.project = p});
  }

}