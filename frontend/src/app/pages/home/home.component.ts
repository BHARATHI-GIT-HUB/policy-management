import { MessageService } from './../../services/message.service';
import { PlanService } from './../../services/plan.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PolicyService } from '../../services/policy.service';
import { CloudFilled } from '@ant-design/icons';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  cardData: any[] = [];
  isLoading: boolean = true;
  isLoggedin: boolean = false;
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

  constructor(
    private http: HttpClient,
    private planService: PlanService,
    private message: NzMessageService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getPlanData();
    this.isLoading = true;
    this.messageService.successMessage$.subscribe((message) => {
      console.log(message, ' message');
      if (message != ' ') this.createMessage('success', message);
    });
  }

  getPlanData(): void {
    this.planService.getAll().subscribe((data) => {
      this.cardData = this.groupDataBysubType(data);
      if (this.cardData) {
        this.isLoading = false;
      }
    });
  }

  groupDataBysubType(data: any[]): any[] {
    return data.reduce((acc: any[], obj: any) => {
      const subtype = obj.subtype.subtype; // Check if this path is correct
      const planName = obj.planName; // Check if this path is correct
      const description = obj.description; // Check if this path is correct
      var imgSrc = '';

      if (subtype === 'Individual') {
        imgSrc = '../../../assets/images/individual.png';
      } else if (subtype === 'Family') {
        imgSrc = '../../../assets/images/family.jpeg';
      } else if (subtype === 'Seniors') {
        imgSrc = '../../../assets/images/senior.jpeg';
      } else {
        imgSrc = '../../../assets/images/travel.avif';
      }

      const newObject = {
        title: subtype + ' Insurance',
        subTitle: planName,
        description: description,
        image: imgSrc,
      };

      if (!acc.some((item) => item.title === newObject.title)) {
        acc.push(newObject);
      }

      return acc;
    }, []);
  }

  createMessage(type: string, message: string): void {
    this.message.create(type, message);
  }
}
