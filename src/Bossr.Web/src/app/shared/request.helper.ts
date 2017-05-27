import { Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';

import { UserManager } from '../usermanager/usermanager';

@Injectable()
export class RequestHelper {
  constructor(private tokenStorage: UserManager) { }

  getHttpOptions(): RequestOptions {
    let token = this.tokenStorage.getToken();

    let options = new RequestOptions({ headers: new Headers() });
    options.headers.append('Authorization', 'Bearer ' + token.token.accessToken);

    return options;
  }
}