import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent {
  contentOpen: boolean = false;
  imageSrc: any;

  @Input()
  title: string = '';

  @Input()
  plans: any[] = [];

  toggleContent() {
    this.contentOpen = !this.contentOpen;
  }

  ngOnInit(): void {
    console.log(this.plans, 'received data');
    if (this.title === 'ICICI') {
      this.imageSrc = '../../../assets/images/ICICI.png';
    } else if (this.title === 'Star Health') {
      this.imageSrc = '../../../assets/images/Star.png';
    } else if (this.title === 'Max Life') {
      this.imageSrc = '../../../assets/images/Max.png';
    } else {
      this.imageSrc = '../../../assets/images/HDFC.png';
    }
  }
}
