import { Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';

import { CurrentUserManager } from '../current-user/current-user-manager';

@Injectable()
export class RequestHelper {
  constructor(private currentUserManager: CurrentUserManager) { }

  getHttpOptions(): RequestOptions {
    let token = this.currentUserManager.getCurrentUser();

    let options = new RequestOptions({ headers: new Headers() });
    options.headers.append('Authorization', 'Bearer ' + token.token.accessToken);

    return options;
  }
}