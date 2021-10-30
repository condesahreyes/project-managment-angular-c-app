import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/services/session/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private sessionService: SessionService, private router: Router) { }

  ngOnInit(): void {
  }

  onLogout(){
    return this.sessionService.postLogout().subscribe(() => {
    this.router.navigate(['/login']) //te animas a sacar esto a tu manera @dieguito
    this.sessionService.removeToken();
    });
  }
}
