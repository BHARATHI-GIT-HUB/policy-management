import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BaseLayoutComponent } from './pages/base-layout/base-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserDetailsComponent } from './pages/user-details/user-details.component';
import { EnrollmentDetailsComponent } from './pages/enrollment-details/enrollment-details.component';
import { BulkUploadComponent } from './pages/bulk-upload/bulk-upload.component';
import { LoginComponent } from './authentication/components/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { AllPlansComponent } from './pages/all-plans/all-plans.component';

const routes: Routes = [
  {
    path: '',
    component: BaseLayoutComponent,
    data: {
      breadcrumb: [{ label: 'Admin', link: '' }],
    },
    children: [
      {
        path: '',
        component: DashboardComponent,
        data: {
          breadcrumb: [{ label: 'Dashboard', link: '' }],
        },
      },
      {
        path: 'user',
        component: UserDetailsComponent,
        data: {
          breadcrumb: [{ label: 'User', link: '/user' }],
        },
      },
      {
        path: 'enrollment',
        component: EnrollmentDetailsComponent,
        data: {
          breadcrumb: [{ label: 'Enrollment', link: '/enrollment' }],
        },
      },
      {
        path: 'bulk-upload',
        component: BulkUploadComponent,
        data: {
          breadcrumb: [{ label: 'Bulk Upload', link: '/bulk-upload' }],
        },
      },
    ],
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [],
  },
  {
    path: 'all-plans',
    component: AllPlansComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
