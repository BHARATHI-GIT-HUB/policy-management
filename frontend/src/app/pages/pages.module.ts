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

@NgModule({
  declarations: [
    BaseLayoutComponent,
    DashboardComponent,
    EnrollmentDetailsComponent,
    UserDetailsComponent,
  ],
  imports: [
    NgZorroModule,
    RouterModule,
    ComponentModule,
    CommonModule,
    NzBreadCrumbComponent,
    NzBreadCrumbItemComponent,
    NzButtonComponent,
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
