import { Component, OnInit } from '@angular/core';
import { InputBoolean } from 'ng-zorro-antd/core/util';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss',
})
export class PaymentComponent implements OnInit {
  step: number = 3;
  radioValue = 'A';
  freqValue = '1';
  years = [1, 2, 3, 4, 5];
  ngOnInit() {}
}
