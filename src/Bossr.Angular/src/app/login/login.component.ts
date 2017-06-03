import { Component } from '@angular/core';
import { TokenService } from '../token/token.service';
import { Token } from '../token/token';
import { CurrentUser } from '../current-user/current-user';
import { CurrentUserManager } from '../current-user/current-user-manager';
import { CurrentUserPersister } from '../current-user/current-user-persister';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [TokenService]
})

export class LoginComponent {
  currentUser: CurrentUser;
  errorMessage: string;

  constructor(private tokenService: TokenService, private currentUserManager: CurrentUserManager, private currentUserPersister: CurrentUserPersister) {
    this.currentUser = currentUserPersister.getCurrentUser();
    this.currentUserManager.getCurrentUser().subscribe(currentUser => this.currentUser = currentUser);
  }

  login(username: string, password: string) {
    this.tokenService
      .postToken(username, password)
      .subscribe(
      token => {
        this.currentUserManager.setCurrentUser({ username: username, token: token });
        this.errorMessage = undefined;
      },
      error => this.errorMessage = error);
  }

  logout() {
    this.currentUserManager.removeCurrentUser();
  }
}
