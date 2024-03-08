import { PlanService } from './../../services/plan.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PolicyService } from '../../services/policy.service';
import { CloudFilled } from '@ant-design/icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  cardData: any[] = [];
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

  constructor(private http: HttpClient, private planService: PlanService) {}

  getPlanData(): void {
    this.planService.getAll().subscribe((data) => {
      this.cardData = this.groupDataBysubType(data);
    });
  }

  groupDataBysubType(data: any[]): any[] {
    return data.reduce((acc: any[], obj: any) => {
      const subtype = obj.subtype.subtype; // Check if this path is correct
      const planName = obj.planName; // Check if this path is correct
      const description = obj.description; // Check if this path is correct
      var imgSrc = '';
      console.log(subtype);
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

  ngOnInit(): void {
    this.getPlanData();
  }
}
