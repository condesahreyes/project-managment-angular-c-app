import { SessionService } from 'src/app/services/session/session.service';
import { UserService } from 'src/app/services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/users/User';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private sessionService: SessionService, 
    private userService: UserService, private router: Router) { }

  user!: User;

  ngOnInit(): void {
    this.sessionService.getUserLogged().subscribe(u => {
        this.user = u;
    });
  }

  onLogout(){
    return this.sessionService.postLogout().subscribe(() => {
    this.router.navigateByUrl('login')
    this.sessionService.removeToken();
    });
  }
}
