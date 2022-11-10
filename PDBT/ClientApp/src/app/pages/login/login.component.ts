import {Component, Inject, OnInit} from '@angular/core';
import {LoginInterface} from "../../services/loginInterface";
import {Login} from "../../models/login";
import {Router} from "@angular/router";
import {LoginService} from "../../services/login.service";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = "";
  password: string = "";
  rememberMe: boolean = false;

  constructor(private loginService:LoginService,
              private router:Router) {
/*    this.loginService.getLoginResults().subscribe((authStatus) => {
      if (authStatus) {
        this.router.navigate(['/']);
      }
    });*/
  }

  ngOnInit(): void {
  }

  async submit(): Promise<void> {
    const loginData: Login = {
      email: this.email,
      password: this.password,
      rememberMe: this.rememberMe
    }

    setTimeout(() => {
      this.loginService.login(loginData);
    }, 100);

    this.loginService.getLoginResults().subscribe((authStatus) => {
      if (authStatus) {
        this.router.navigate(['/']);
      }
    });
  }
}
