import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {Login} from "../../models/session/Login";
import { SessionService } from '../../services/session.service';
import { ProjectService } from '../../services/project.service';
import { Project } from 'src/app/models/project/Project';
import { ProjectOut } from 'src/app/models/project/ProjectOut';
import { User } from 'src/app/models/users/User';
import { Bug } from 'src/app/models/bug/Bug';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private sessionService: SessionService,private projectService: ProjectService,
     private router: Router) { }

  login: Login = {
    Email: "",
    Password: ""
  }

  idProject = "";

  project: Project = {
    Name: ""
  }

   user: User={
    Rol :"",
    Name : "",
    LastName : "",
    UserName : "",
    Password : "",
    Email : ""
   }


  projectGets: ProjectOut[] = []

  projectGet : ProjectOut ={
    Id: 0,
    Users : [],
    Bugs : [],
    TotalBugs : 0,
    Name : ""
  }

  ngOnInit(): void {
   this.getProjects() 
  }

  onLogin() {
     return this.sessionService.postLogin(this.login).subscribe(data => {
      this.sessionService.saveToken(data.token);
      this.router.navigate(['/home']);
    }, error => {
      console.error(error)
    });
  }

  onLogout(){
    return this.sessionService.postLogout().subscribe(() => {
    this.router.navigate(['/login'])
    this.sessionService.removeToken();
    });
  }

  onProject(){
    return this.projectService.createProject(this.project).subscribe();
  }

  getProject(){
    return this.projectService.getProject(this.idProject).subscribe(m => {
      this.projectGet = m;
      console.log(m.Name + " PEPEeJEJEJE");
    });
  }

  deleteProject(){
    return this.projectService.deleteProject(this.idProject).subscribe(() => {
      console.log(this.projectGet.Name + "JEJEJE");
    });
  }


  getProjects() {
    this.projectService.getProjects().subscribe(m => {
      this.projectGets = m
      console.log("Esooo " + m[0].Name)
    }, error => {
      console.error(error);
      alert(error);
    });
  }
}
