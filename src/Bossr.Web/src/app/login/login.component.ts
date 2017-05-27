import { Component } from '@angular/core';
import { TokenService } from './token.service';
import { Token } from './token';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [TokenService]
})

export class LoginComponent {
  currentUser;
  errorMessage: string;

  constructor(private tokenService: TokenService) {
    this.getUser();
  }

  login(username: string, password: string) {
    this.tokenService
      .postToken(username, password)
      .subscribe(
      token => {
        this.setUser(username, token);
        this.getUser();
        this.errorMessage = undefined;
      },
      error => this.errorMessage = error);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.getUser();
  }

  setUser(username: string, token: Token) {
    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));
  }

  getUser() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }
}
