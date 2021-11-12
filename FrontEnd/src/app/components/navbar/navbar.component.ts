import { SessionService } from 'src/app/services/session/session.service';
import { User } from 'src/app/models/users/User';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    private sessionService: SessionService, 
    private router: Router
    ) { }

  user!: User;
  userInfo: string = "";
  userRol: string = "";

  ngOnInit() {
    this.getUserLogger();
  }

  getUserLogger() {
    this.sessionService.getUserLogged().subscribe(u => {
      this.userInfo = u.rol + " - " + u.userName;
      this.user = u;
      this.userRol= u.rol;
    });
  }
  
  onLogout() {
    return this.sessionService.postLogout().subscribe(() => {
      this.router.navigateByUrl('login')
      this.sessionService.removeToken();
    });
  }

}
