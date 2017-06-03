import { Output, EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

import { CurrentUser } from './current-user';
import { CurrentUserPersister } from './current-user-persister';

@Injectable()
export class CurrentUserManager {
  private currentUser = new Subject<CurrentUser>();

  constructor(private currentUserPersister: CurrentUserPersister) {
    this.currentUser.next(this.currentUserPersister.getCurrentUser());
  }

  setCurrentUser(currentUser: CurrentUser) {
    this.currentUserPersister.setCurrentUser(currentUser);
    this.currentUser.next(this.currentUserPersister.getCurrentUser());
  }

  getCurrentUser(): Observable<CurrentUser> {
    return this.currentUser.asObservable();
  }

  removeCurrentUser() {
    this.currentUserPersister.removeCurrentUser();
    this.currentUser.next();
  }
}