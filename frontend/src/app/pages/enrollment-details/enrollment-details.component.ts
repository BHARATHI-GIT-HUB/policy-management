import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataItem, policy } from '../../model';
import { HttpClient } from '@angular/common/http';
import { json } from '@angular-devkit/core';
import { ApiService } from '../../services/api.service';
import { PolicyService } from '../../services/policy.service';
@Component({
  selector: 'app-enrollment-details',
  templateUrl: './enrollment-details.component.html',
  styleUrl: './enrollment-details.component.scss',
})
export class EnrollmentDetailsComponent implements OnInit {
  breadcrumbName: string = '';
  Header: string[] = [];
  listOfData: policy[] = [];

  constructor(
    private route: ActivatedRoute,
    private policyService: PolicyService
  ) {}

  ngOnInit(): void {
    this.Header = [
      'Provider',
      'Client',
      'Coverage Amount',
      'Permium',
      'Enrolled On',
      'Commision',
    ];
    this.breadcrumbName = this.route.snapshot.data['title'];
    this.policyService.getAll().subscribe(
      (data: policy[]) => {
        this.listOfData = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
