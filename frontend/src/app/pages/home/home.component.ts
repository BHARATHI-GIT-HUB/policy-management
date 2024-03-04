import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  panels = [
    {
      active: true,
      name: 'This is panel header 1',
      disabled: false,
    },
    {
      active: false,
      disabled: false,
      name: 'This is panel header 2',
    },
    {
      active: false,
      disabled: true,
      name: 'This is panel header 3',
    },
  ];
}
