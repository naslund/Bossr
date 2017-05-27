import { Response } from '@angular/http';
import { Injectable } from '@angular/core';

import { CurrentUserManager } from '../current-user/current-user-manager';

@Injectable()
export class ErrorHandler {
  constructor(private currentUserManager: CurrentUserManager) { }

  handleError(error: Response | any) {
    let errorMessage: string;
    if (error instanceof Response) {
      if (error.status === 400) {
        errorMessage = error.json().message;
      } else if (error.status === 401) {
        this.currentUserManager.removeCurrentUser();
      }
    }
    return Promise.reject(errorMessage);
  }
}

export class DataExtractor {
  extractData(response: Response) {
    let body = response.json();
    return body || {};
  }
}