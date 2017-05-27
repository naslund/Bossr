import { Token } from '../token/token';

export class UserManager {
  setToken(username: string, token: Token) {
    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));
  }

  getToken() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  isThereAnyToken() {
    return !(localStorage.getItem('currentUser') === null);
  }
}