import { Component } from '@angular/core';
import { TokenService } from '../token/token.service';
import { Token } from '../token/token';
import { CurrentUser } from '../current-user/current-user';
import { CurrentUserManager } from '../current-user/current-user-manager';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [TokenService, CurrentUserManager]
})

export class LoginComponent {
  currentUser;
  errorMessage: string;

  constructor(private tokenService: TokenService, private currentUserManager: CurrentUserManager) {
    this.currentUser = this.currentUserManager.getCurrentUser();
  }

  login(username: string, password: string) {
    this.tokenService
      .postToken(username, password)
      .subscribe(
      token => {
        this.currentUserManager.setCurrentUser({ username: username, token: token });
        this.currentUser = this.currentUserManager.getCurrentUser();
        this.errorMessage = undefined;
      },
      error => this.errorMessage = error);
  }

  logout() {
    this.currentUserManager.removeCurrentUser();
    this.currentUser = this.currentUserManager.getCurrentUser();
  }
}
