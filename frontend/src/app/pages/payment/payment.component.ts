import { environment } from './../../../environments/environment';
import { PolicyService } from './../../services/policy.service';
import { Client } from './../../model/client';
import { ClientService } from './../../services/client.service';
import { user } from './../../model/user';
import { Component, OnInit } from '@angular/core';
import { InputBoolean } from 'ng-zorro-antd/core/util';
import { PlanService } from '../../services/plan.service';
import { ActivatedRoute } from '@angular/router';
import { timePeriod } from '../../helpers/conditionalReturn';
import { policy } from '../../model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss',
})
export class PaymentComponent implements OnInit {
  step: number = 3;
  selectedYear: number = 1;
  selectedfreq: string = 'Yearly';
  years: number[] = [1, 2, 3, 4, 5];
  frequency: string[] = ['Yearly', 'Half Yearly', 'Quarterly', 'Monthly'];
  planId: number = 0;
  selectedCoverage: number = 0;
  planData: any;
  clientData: any;
  premium: number = 0;
  totalPremium: number = 0;
  discount: number = 0;
  gst: number = 0;
  age: number = 0;
  responseMessage: string = '';
  errorMessage: string = '';

  // clientId = 6;
  clientId = JSON.parse(String(localStorage.getItem('user'))).id;

  constructor(
    private route: ActivatedRoute,
    private planService: PlanService,
    private clientService: ClientService,
    private policyService: PolicyService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.planId = params['id'];
      this.selectedCoverage = params['coverage'];
    });
    this.getPlanById(this.planId);
    this.getClientById(this.clientId);
    this.calculatePremium(timePeriod(this.selectedfreq), this.selectedYear);
  }

  enrollPlan() {
    const url = environment.apiurl;
    const bodyData = {
      planId: this.planId,
      agentId: 4,
      clientId: this.clientId,
      coverageAmount: this.selectedCoverage,
      frequency: this.selectedfreq,
      premium: this.totalPremium,
      commisionAmount: 9000,
      timePeriod: this.selectedYear,
    };

    this.http.post<any>(`${url}api/policy`, bodyData).subscribe(
      (res) => {
        this.responseMessage = res.message;
        console.log(res, 'policy creation');
      },
      (err) => (this.errorMessage = err.error.message)
    );

    // this.policyService.create(bodyData).subscribe((data) => {
    //   console.log(data.message);
    // });
  }

  getPlanById(id: number) {
    this.planService.getById(id).subscribe((data) => {
      this.planData = data;
    });
  }
  getClientById(id: number) {
    this.clientService.getById(id).subscribe((data) => {
      this.clientData = data;
      this.age = this.calculateAge(this.clientData.dob);
    });
  }

  calculateAge(dateOfBirth: string) {
    const today = new Date();
    const birthDate = new Date(dateOfBirth);
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDifference = today.getMonth() - birthDate.getMonth();

    if (
      monthDifference < 0 ||
      (monthDifference === 0 && today.getDate() < birthDate.getDate())
    ) {
      age--;
    }
    console.log(age, 'age');

    return age;
  }
  onYearChange(): void {
    this.calculatePremium(timePeriod(this.selectedfreq), this.selectedYear);
  }
  onFrequencyChange(): void {
    this.calculatePremium(timePeriod(this.selectedfreq), this.selectedYear);
  }

  calculatePremium(freq: number, year: number) {
    this.discount = Math.round(this.premium * 0.05);
    this.gst = Math.round(this.premium * 0.18);

    this.premium = Math.round(this.selectedCoverage / (freq * year));
    // Calculate total premium
    this.totalPremium = Math.round(this.premium - this.discount + this.gst);
  }
}
