import { CurrentUser } from './current-user';

export class CurrentUserManager {
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