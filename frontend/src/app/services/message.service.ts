import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private successMessageSource = new Subject<string>();
  constructor() {}

  successMessage$ = this.successMessageSource.asObservable();

  sendMessage(message: string) {
    this.successMessageSource.next(message);
  }
}
