import { NgModule } from '@angular/core';
import { BaseLayoutComponent } from './base-layout/base-layout.component';
import { NgZorroModule } from '../NgZorro.module';
import { RouterModule } from '@angular/router';
import { ComponentModule } from '../components/components.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EnrollmentDetailsComponent } from './enrollment-details/enrollment-details.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import {
  NzBreadCrumbComponent,
  NzBreadCrumbItemComponent,
} from 'ng-zorro-antd/breadcrumb';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { CommonModule } from '@angular/common';
import { BulkUploadComponent } from './bulk-upload/bulk-upload.component';
import { HomeComponent } from './home/home.component';
import { SelectPlanComponent } from './select-plan/select-plan.component';
import { AllPlansComponent } from './all-plans/all-plans.component';
import { PaymentComponent } from './payment/payment.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    BaseLayoutComponent,
    DashboardComponent,
    EnrollmentDetailsComponent,
    UserDetailsComponent,
    BulkUploadComponent,
    HomeComponent,
    SelectPlanComponent,
    AllPlansComponent,
    PaymentComponent,
  ],
  imports: [
    NgZorroModule,
    RouterModule,
    ComponentModule,
    CommonModule,
    NzBreadCrumbComponent,
    NzBreadCrumbItemComponent,
    NzButtonComponent,
    FormsModule,
  ],
  providers: [],
  exports: [
    BaseLayoutComponent,
    DashboardComponent,
    UserDetailsComponent,
    EnrollmentDetailsComponent,
  ],
})
export class PagesModule {}
