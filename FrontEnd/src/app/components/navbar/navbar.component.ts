import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/services/session/session.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private sessionService: SessionService, private userService: UserService, private router: Router) { }

  user: string = "";

  ngOnInit(): void {
    this.sessionService.getUserIdLogged().subscribe(u => {
      this.userService.getById(u.userId + "").subscribe(x =>
        this.user = x.rol + " - " + x.userName
      );
    });
  }

  onLogout(){
    return this.sessionService.postLogout().subscribe(() => {
    this.router.navigateByUrl('login')
    this.sessionService.removeToken();
    });
  }
}
