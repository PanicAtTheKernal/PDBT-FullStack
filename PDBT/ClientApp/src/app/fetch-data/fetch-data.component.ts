import { Component, Inject } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders ({
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  })
}

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    http.post<Login>(baseUrl + 'api/user/login', {
      email: 'user@example.com',
      password: 'string'
    }, httpOptions).subscribe( (result) => {
      console.log(result["token"]);
    });
  }
}

interface Login {
  token: string;
  jwt: string;
  userId: string;
  expires: Date;
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
