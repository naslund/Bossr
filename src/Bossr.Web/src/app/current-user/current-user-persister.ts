import { Injectable } from '@angular/core';

import { CurrentUser } from './current-user';

@Injectable()
export class CurrentUserPersister {
  setCurrentUser(currentUser: CurrentUser) {
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
  }

  getCurrentUser(): CurrentUser {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  removeCurrentUser() {
    localStorage.removeItem('currentUser');
  }
}