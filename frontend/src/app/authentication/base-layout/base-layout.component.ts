import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-base-layout',
  templateUrl: './base-layout.component.html',
  styleUrl: './base-layout.component.scss',
})
export class BaseLayoutComponent implements OnInit {
  errorMessage: string = '';
  responseMessage: string = '';

  onError(evenData: { message: string }) {
    this.errorMessage = evenData.message;
  }

  ngOnInit(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
}
