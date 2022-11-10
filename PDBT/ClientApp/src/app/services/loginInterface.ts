import {Login} from "../models/login";
import {Observable} from "rxjs";
import {LoginResponse} from "../models/login-response";

export interface LoginInterface {
  login(loginData: Login): void
}
