import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { DataExtractor, ErrorHandler } from '../shared/response.helper';
import { RequestHelper } from '../shared/request.helper';

import { World } from './world';

@Injectable()
export class WorldService {
  private worldsUrl = 'http://localhost:5000/api/worlds';

  constructor(private http: Http, private errorHandler: ErrorHandler, private dataExtractor: DataExtractor, private requestHelper: RequestHelper) { }

  getAllWorlds(): Observable<World[]> {
    let options = this.requestHelper.getHttpOptions();

    return this.http
      .get(this.worldsUrl, options)
      .map(this.dataExtractor.extractData)
      .catch(this.errorHandler.handleError);
  }
}
