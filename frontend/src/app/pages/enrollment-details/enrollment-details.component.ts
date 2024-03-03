import { provider } from './../../../../../frontend/src/app/model/provider';

import { Agent } from './../../../../../frontend/src/app/model/agent';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataItem, policy } from '../../model';
import { HttpClient } from '@angular/common/http';
import { json } from '@angular-devkit/core';
import { ApiService } from '../../services/api.service';
import { PolicyService } from '../../services/policy.service';
import { __values } from 'tslib';
import { forEach } from '@angular-devkit/schematics';
@Component({
  selector: 'app-enrollment-details',
  templateUrl: './enrollment-details.component.html',
  styleUrl: './enrollment-details.component.scss',
})
export class EnrollmentDetailsComponent implements OnInit {
  breadcrumbName: string = '';
  listOfColumn: any[] = [];
  listOfData: policy[] = [];
  editableColumn: string[] = [];
  responseMessage: string = '';
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private policyService: PolicyService
  ) {}

  FetchAllPolicyData() {
    this.policyService.getAll().subscribe(
      (data: any) => {
        data.forEach((element: any) => {
          let newPolicy: any = {
            id: element.id,
            PlanName: element.plan.planName,
            ProviderName: element.plan.user.companyName,
            AgentName: element.agent.user.username,
            ClientName: element.client.user.username,
            Premium: element.premium,
            CoverageAmount: element.coverageAmount,
            Frequency: element.frequency,
            TimePeriod: element.timePeriod,
          };
          this.listOfData.push(newPolicy);
        });
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onDelete(id: number) {
    this.policyService.delete(id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.listOfData = [];
        this.FetchAllPolicyData();
      },
      (err) => (this.errorMessage = err.message)
    );
  }
  onUpdate(updatedVlaue: any) {
    const databody: any = {
      frequency: updatedVlaue.Frequency,
      coverageAmount: updatedVlaue.CoverageAmount,
      timePeriod: updatedVlaue.TimePeriod,
    };

    this.policyService.update(databody, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.FetchAllPolicyData();
      },
      (err) => (this.errorMessage = err.message)
    );
  }

  ngOnInit(): void {
    this.editableColumn = ['Frequency', 'TimePeriod'];
    this.listOfColumn = [
      {
        title: 'Plan Name',
      },
      {
        title: 'Provider',
      },
      {
        title: 'Client',
      },
      {
        title: 'Agent',
      },
      {
        title: 'Premium',
      },
      {
        title: 'Coverage Amount',
        editable: true,
        sortOrder: null,
        sortFn: (a: policy, b: policy) => b.CoverageAmount - a.CoverageAmount,
        sortDirections: ['ascend', 'descend', null],
      },
      {
        title: 'Frequency',
        editable: true,
        listOfFilter: [
          { text: 'Yearly', value: 'Yearly' },
          { text: 'Half Yearly', value: 'Half Yearly' },
          { text: 'Quarly', value: 'Quartly' },
          { text: 'Monthly', value: 'Monthly' },
        ],
        filterFn: (frequency: string, item: policy) =>
          item.Frequency.indexOf(frequency) !== -1,
      },
      {
        title: 'Time Period',
        editable: true,
      },
      {
        title: 'Action',
      },
    ];

    this.breadcrumbName = this.route.snapshot.data['title'];
    this.FetchAllPolicyData();
  }
}
