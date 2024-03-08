import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';
import { BaseLayoutComponent } from './pages/base-layout/base-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserDetailsComponent } from './pages/user-details/user-details.component';
import { EnrollmentDetailsComponent } from './pages/enrollment-details/enrollment-details.component';
import { BulkUploadComponent } from './pages/bulk-upload/bulk-upload.component';
import { LoginComponent } from './authentication/components/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { AllPlansComponent } from './pages/all-plans/all-plans.component';
import { BaseLayoutComponent as AuthComponent } from './authentication/base-layout/base-layout.component';
import { AuthGuard, RoleGuard } from './helpers/auth.guard';
import { SelectPlanComponent } from './pages/select-plan/select-plan.component';
import { PaymentComponent } from './pages/payment/payment.component';

const routes: Routes = [
  { path: 'login', component: AuthComponent },
  {
    path: '',
    component: BaseLayoutComponent,
    data: {
      breadcrumb: [
        {
          // label: `${JSON.parse(String(localStorage.getItem('user'))).role}`,
          label: ``,
          link: '',
        },
      ],
    },
    children: [
      {
        path: '',
        component: DashboardComponent,
        data: {
          breadcrumb: [{ label: 'Dashboard', link: '' }],
          roles: ['Admin', 'Provider', 'Agent'],
        },
        canActivate: [AuthGuard, RoleGuard],
      },
      {
        path: 'user',
        component: UserDetailsComponent,
        data: {
          breadcrumb: [{ label: 'User', link: '/user' }],
          roles: ['Admin'],
        },
        canActivate: [AuthGuard, RoleGuard],
      },
      {
        path: 'enrollment',
        component: EnrollmentDetailsComponent,
        data: {
          breadcrumb: [{ label: 'Enrollment', link: '/enrollment' }],
          roles: ['Admin'],
        },
        canActivate: [AuthGuard, RoleGuard],
      },
      {
        path: 'bulk-upload',
        component: BulkUploadComponent,
        data: {
          breadcrumb: [{ label: 'Bulk Upload', link: '/bulk-upload' }],
          roles: ['Admin', 'Provider', 'Agent'],
        },
        canActivate: [AuthGuard, RoleGuard],
      },
    ],
  },
  {
    path: 'home',
    component: HomeComponent,
    children: [],
  },
  {
    path: 'select-plans',
    component: SelectPlanComponent,
    data: {
      roles: ['Client'],
    },
    // canActivate: [AuthGuard, RoleGuard],
  },
  {
    path: 'all-plans',
    component: AllPlansComponent,
    data: {
      roles: ['Client'],
    },
    // canActivate: [AuthGuard, RoleGuard],
  },
  {
    path: 'payment',
    component: PaymentComponent,
    data: {
      roles: ['Client'],
    },
    // canActivate: [AuthGuard, RoleGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
