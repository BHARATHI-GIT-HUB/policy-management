import { plan } from './../../model/plan';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlanService } from '../../services/plan.service';
import { ProviderService } from '../../services/provider.service';
import { count } from 'rxjs';
import { sum } from 'ng-zorro-antd/core/util';
import { PolicyService } from '../../services/policy.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  breadcrumbName: string = '';
  listOfProviders: string[] = [];
  totalPlans: number[] = [];
  planCount: number = 0;
  ProviderCount: number = 0;
  EnrollmentCount: number = 0;
  doughnutData: number[] = [];
  title = 'export-excel';
  fileName = 'ExportExce.xlsx';

  constructor(
    private route: ActivatedRoute,
    private planService: PlanService,
    private policyService: PolicyService
  ) {}

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

  getPlanData(): void {
    this.planService.getAll().subscribe((data) => {
      this.groupDataByProvider(data).forEach((value, key) => {
        this.listOfProviders.push(key);
        this.totalPlans.push(value.length);
      });
      this.planCount = sum(this.totalPlans);
      this.ProviderCount = this.listOfProviders.length;

      this.doughnutData.push(
        this.planCount * 10,
        this.ProviderCount * 10,
        this.EnrollmentCount * 10
      );
    });
  }

  getPolicyData() {
    this.policyService.getAll().subscribe((data) => {
      this.EnrollmentCount = data.length;
    });
  }

  ngOnInit() {
    this.breadcrumbName = this.route.snapshot.data['title'];
    this.getPlanData();
    this.getPolicyData();
  }
}
