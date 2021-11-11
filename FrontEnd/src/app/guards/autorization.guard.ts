import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AutorizationGuard implements CanActivate {

  constructor(
    private router: Router
  ){

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
   
      const rol = localStorage.getItem('Token'); // VER ACA SI LO DEJAMOS EN LOCAL O VER DE PEGARLE AL SERVICIO Y OBTENER EL TOKEN.
    
    if (!rol?.includes('Administrador')) {
      this.router.navigateByUrl("dashboard");

      //Crear un componente y dirigirlo para ahi que diga no tenes permisos. tirando un mensaje de error 
      return false;
    }
    return true;

  }

}
