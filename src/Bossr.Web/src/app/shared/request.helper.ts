import { Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';

import { CurrentUserPersister } from '../current-user/current-user-persister';

@Injectable()
export class RequestHelper {
  constructor(private currentUserPersister: CurrentUserPersister) { }

  getHttpOptions(): RequestOptions {
    let options = new RequestOptions({ headers: new Headers() });

    let currentUser = this.currentUserPersister.getCurrentUser();
    if (currentUser) {
      options.headers.append('Authorization', 'Bearer ' + currentUser.token.accessToken);
    }

    return options;
  }
}