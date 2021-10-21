import {Injectable} from "@angular/core";
import {HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {SessionService} from "./session.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private auth: SessionService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const authToken = this.auth.getToken();
    if (authToken) {
      req = req.clone({
        headers: req.headers.set('Authorization', authToken)
      });
    }
    return next.handle(req);
  }
}