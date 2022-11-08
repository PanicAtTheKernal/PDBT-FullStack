import {Component, Inject, OnInit} from '@angular/core';
import {LoginInterface} from "../../services/loginInterface";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(@Inject('LoginInterface') private login:LoginInterface) {
    login.printHello();
  }

  ngOnInit(): void {
  }


}
