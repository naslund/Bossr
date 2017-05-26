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
  title = 'Login';

  token: Token;
  currentUser: string;
  isLoggedIn: boolean = false;
  errorMessage: string;

  constructor(private tokenService: TokenService) { }

  login(username: string, password: string) {
    this.tokenService
      .postToken(username, password)
      .subscribe(
      token => {
        this.token = token;
        this.currentUser = username;
        this.isLoggedIn = true;
        this.errorMessage = undefined;
      },
      error => this.errorMessage = error);
  }

  logout() {
    this.currentUser = undefined;
    this.isLoggedIn = false;
  }
}
