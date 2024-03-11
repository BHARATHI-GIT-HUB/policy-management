import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private successMessage: string = '';
  constructor() {}

  sendMessage(message: string) {
    this.successMessage = message;
  }

  getSuccessMessage(): string {
    return this.successMessage;
  }
}
