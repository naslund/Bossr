import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Token } from './token';

@Injectable()
export class TokenService {
  private tokenUrl = 'http://localhost:5000/token';

  constructor(private http: Http) { }

  postToken(username: string, password: string): Observable<Token> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    let options = new RequestOptions({ headers: headers });

    let body = "username=" + username + "&password=" + password;

    return this.http
      .post(this.tokenUrl, body, options)
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();
    return body || {};
  }

  private handleError(error: Response | any) {
    let errorMessage: string;
    if (error instanceof Response) {
      if (error.status === 400) {
        errorMessage = error.json().message;
      }
    }
    return Promise.reject(errorMessage);
  }
}