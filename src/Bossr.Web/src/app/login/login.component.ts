import { Component } from '@angular/core';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  title = 'Login';

  currentUser = '';
  isLoggedIn = false;

  login(username: string, password: string) {
    this.currentUser = username;
    this.isLoggedIn = true;
  }

  logout() {
    this.currentUser = '';
    this.isLoggedIn = false;
  }
}
