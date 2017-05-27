import { Component } from '@angular/core';
import { TokenService } from '../token/token.service';
import { Token } from '../token/token';
import { UserManager } from '../usermanager/usermanager';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [TokenService, UserManager]
})

export class LoginComponent {
  currentUser;
  errorMessage: string;

  constructor(private tokenService: TokenService, private tokenStorage: UserManager) {
    this.currentUser = this.tokenStorage.getToken();
  }

  login(username: string, password: string) {
    this.tokenService
      .postToken(username, password)
      .subscribe(
      token => {
        this.tokenStorage.setToken(username, token);
        this.currentUser = this.tokenStorage.getToken();
        this.errorMessage = undefined;
      },
      error => this.errorMessage = error);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUser = this.tokenStorage.getToken();
  }
}
