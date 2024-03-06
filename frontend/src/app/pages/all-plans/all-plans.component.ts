import { forEach } from '@angular-devkit/schematics';
import { filter } from 'rxjs';
import { PlanService } from './../../services/plan.service';
import { Component, OnInit } from '@angular/core';
import { plan } from '../../model/plan';

@Component({
  selector: 'app-all-plans',
  templateUrl: './all-plans.component.html',
  styleUrl: './all-plans.component.scss',
})
export class AllPlansComponent implements OnInit {
  providerGroupedData: any = [];
  keys: string[] = [];
  types: string[] = ['Health', 'Travel', 'Corprate'];
  subTypes: string[] = ['Individual', 'Family', 'Seniors'];
  step: number = 1;

  constructor(private planService: PlanService) {}

  ngOnInit(): void {
    this.getPlans();
  }

  getPlans(): void {
    this.planService.getAll().subscribe((response) => {
      this.providerGroupedData = this.groupDataByProvider(response);
      this.keys = [...this.providerGroupedData.keys()];
    });
  }

  groupDataByProvider(data: any[]): Map<string, any[]> {
    return data.reduce((acc: Map<string, any[]>, obj: any) => {
      const companyName = obj.provider.companyName;

      if (!acc.has(companyName)) {
        acc.set(companyName, []);
      }

      acc.get(companyName)?.push(obj);

      return acc;
    }, new Map<string, any[]>());
  }

  filterByType(type: string) {
    this.getPlans();
    const filteredData: Map<string, any[]> = new Map<string, any[]>();

    this.providerGroupedData.forEach((plans: any[], key: string) => {
      const filteredPlans = plans.filter(
        (item: any) => item.subtype.type.type === type
      );

      if (filteredPlans.length > 0) {
        filteredData.set(key, filteredPlans);
      } else {
        filteredData.set(key, []);
      }
    });

    this.providerGroupedData = filteredData;
  }
  filterBySubtype(subType: string) {
    const filteredData: Map<string, any[]> = new Map<string, any[]>();

    this.providerGroupedData.forEach((plans: any[], key: string) => {
      const filteredPlans = plans.filter(
        (item: any) => item.subtype.subtype === subType
      );

      if (filteredPlans.length > 0) {
        filteredData.set(key, filteredPlans);
      }
    });

    this.providerGroupedData = filteredData;

    console.log(this.providerGroupedData);
  }
}
