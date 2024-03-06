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
import { BaseLayoutComponent as AuthComponent } from './authentication/base-layout/base-layout.component';
import {
  adminGuard,
  agentGuard,
  authGuard,
  clientGuard,
  providerGuard,
} from './helpers/auth.guard';

const routes: Routes = [
  { path: 'login', component: AuthComponent },
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
        canActivate: [authGuard, adminGuard],
      },
      {
        path: 'user',
        component: UserDetailsComponent,
        data: {
          breadcrumb: [{ label: 'User', link: '/user' }],
        },
        canActivate: [authGuard, adminGuard, providerGuard],
      },
      {
        path: 'enrollment',
        component: EnrollmentDetailsComponent,
        data: {
          breadcrumb: [{ label: 'Enrollment', link: '/enrollment' }],
        },
        canActivate: [authGuard],
      },
      {
        path: 'bulk-upload',
        component: BulkUploadComponent,
        data: {
          breadcrumb: [{ label: 'Bulk Upload', link: '/bulk-upload' }],
        },
        canActivate: [authGuard],
      },
    ],
  },
  {
    path: 'home',
    component: HomeComponent,
    children: [],
  },
  {
    path: 'all-plans',
    component: AllPlansComponent,
    canActivate: [authGuard, clientGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
