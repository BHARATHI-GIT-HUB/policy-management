import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BaseLayoutComponent } from './pages/base-layout/base-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserDetailsComponent } from './pages/user-details/user-details.component';
import { EnrollmentDetailsComponent } from './pages/enrollment-details/enrollment-details.component';
import { BulkUploadComponent } from './pages/bulk-upload/bulk-upload.component';

const routes: Routes = [
  {
    path: '',
    component: BaseLayoutComponent,
    data: { title: '' },
    children: [
      {
        path: '',
        component: DashboardComponent,
        data: { title: 'Dashboard' },
      },
      {
        path: 'user',
        component: UserDetailsComponent,
        data: { title: 'User Details' },
      },
      {
        path: 'enrollment',
        component: EnrollmentDetailsComponent,
        data: { title: 'Enrollment Details' },
      },
      {
        path: 'bulk-upload',
        component: BulkUploadComponent,
        data: { title: 'Enrollment Details' },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
