import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable, Subscription} from 'rxjs';
import {LoginService} from "./login.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  isAuthenticated: boolean= false;
  loginSub: Subscription;

  constructor(private loginService: LoginService,
              private router: Router) {
    this.loginSub = this.loginService.getLoginResults().subscribe((authStatus) => {
      this.isAuthenticated = authStatus;

    })
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    //Give it time for the isAuthenticate variable to update
    setTimeout(() => {
      console.log("AuthStat: " + this.isAuthenticated);
      if (!this.isAuthenticated) {
        this.router.navigate(['/login']);
      }
    }, 1);
    return this.isAuthenticated;
  }

}
