import {Inject, Injectable} from '@angular/core';
import {LoginInterface} from "./loginInterface";
import {ApiServiceInterface} from "./api-service-interface";
import {Login} from "../models/login";
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LoginResponse} from "../models/login-response";
import {Observable, Subject, Subscription} from "rxjs";

const httpOptions = {
  headers: new HttpHeaders ({
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class LoginService implements LoginInterface, ApiServiceInterface {
  private loginResults:Subject<boolean>;
  private jwt: string = "";
  private refreshToken: string = "";
  private authStatus: boolean = false;
  private baseUrl: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.loginResults = new Subject<boolean>();
    this.loginResults.next(this.authStatus);
  }

  login(loginData: Login): void {
    let results = this.http.post<LoginResponse>(this.baseUrl + 'api/User/login', {
      email: loginData.email,
      password: loginData.password
    }, httpOptions).subscribe({
      next: () => {
        this.authStatus = true;
        this.loginResults.next(this.authStatus);
      }
    });
  }

  loginSuccessHandle(data: LoginResponse): void {
    this.authStatus = true;
    console.log(this.authStatus);
  }

  logout(): void {

  }

  userSignedIn(): boolean {
    return false;
  }

  public getLoginResults(): Observable<boolean> {
    return this.loginResults.asObservable();
  }

  getAuthToken(): string {
    return this.jwt;
  }

  public getAuthStatus(): boolean {
    return this.authStatus;
  }
}
