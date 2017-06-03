import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { DataExtractor, ErrorHandler } from '../shared/response.helper';

import { Token } from './token';

@Injectable()
export class TokenService {
  private tokenUrl = 'http://localhost:5000/token';

  constructor(private http: Http, private errorHandler: ErrorHandler, private dataExtractor: DataExtractor) { }

  postToken(username: string, password: string): Observable<Token> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    let options = new RequestOptions({ headers: headers });

    let body = "username=" + username + "&password=" + password;

    return this.http
      .post(this.tokenUrl, body, options)
      .map(result => this.dataExtractor.extractData(result))
      .catch(error => this.errorHandler.handleError(error));
  }
}