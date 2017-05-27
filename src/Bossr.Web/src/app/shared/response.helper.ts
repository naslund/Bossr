import { Response } from '@angular/http';

export class ErrorHandler {
  handleError(error: Response | any) {
    let errorMessage: string;
    if (error instanceof Response) {
      if (error.status === 400) {
        errorMessage = error.json().message;
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