import { Injectable } from '@angular/core';
import {LoginInterface} from "./loginInterface";
import {ApiServiceInterface} from "./api-service-interface";

@Injectable({
  providedIn: 'root'
})
export class LoginService implements LoginInterface, ApiServiceInterface {
  private jwt: string = "";
  private refreshToken: string = "";

  constructor() { }

  printHello() {
    console.log("Hello world ts interface");
  }

  login(): void {

  }

  logout(): void {

  }

  userSignedIn(): boolean {
    return false;
  }

  getAuthToken(): string {
    return this.jwt;
  }
}
