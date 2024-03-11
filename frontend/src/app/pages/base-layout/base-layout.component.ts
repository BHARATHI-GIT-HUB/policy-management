import { MessageService } from './../../services/message.service';
import { PolicyService } from './../../services/policy.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import * as ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-base-layout',
  templateUrl: './base-layout.component.html',
  styleUrls: ['./base-layout.component.scss'],
})
export class BaseLayoutComponent implements OnInit {
  breadcrumbs: any[] = [];
  isCollapsed = false;
  currentPath: string = '';
  userRole: string = '';
  EnrolledData: any[] = [];

  toggleCollapsed(): void {
    this.isCollapsed = !this.isCollapsed;
  }
  menu: {
    title: string;
    icon: string;
    path?: string;
  }[] = [];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private policyService: PolicyService,
    private messageService: MessageService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.FetchAllPolicyData();
    const user: any = localStorage.getItem('user');
    this.userRole = JSON.parse(String(user)).role;
    this.messageService.successMessage$.subscribe((message) => {
      if (message != ' ') this.createMessage('success', message);
    });
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.currentPath = event.url.slice(1);

        this.updateBreadcrumbs();
      });

    if (this.userRole === 'Admin') {
      this.menu = [
        {
          title: 'Dashboard',
          icon: 'dashboard',
          path: '',
        },
        {
          title: 'Enrollment',
          icon: 'file-done',
          path: 'enrollment',
        },
        {
          title: 'Bulk Upload',
          icon: 'upload',
          path: 'bulk-upload',
        },
        {
          title: 'Users',
          icon: 'user',
          path: 'user',
        },
      ];
    } else if (this.userRole === 'Provider') {
      this.menu = [
        {
          title: 'Dashboard',
          icon: 'dashboard',
          path: '',
        },
      ];
    } else if (this.userRole === 'Agent') {
      this.menu = [
        {
          title: 'Dashboard',
          icon: 'dashboard',
          path: '',
        },
        {
          title: 'Bulk Upload',
          icon: 'upload',
          path: 'bulk-upload',
        },
      ];
    }
  }

  FetchAllPolicyData() {
    this.policyService.getAll().subscribe(
      (data: any) => {
        data.forEach((element: any) => {
          let newPolicy: any = {
            id: element.id,
            PlanName: element.plan.planName,
            ProviderName: element.plan.user.companyName,
            AgentName: element.agent.user.username,
            ClientName: element.client.user ? element.client.user.username : '',
            Premium: element.premium,
            CoverageAmount: element.coverageAmount,
            Frequency: element.frequency,
            TimePeriod: element.timePeriod,
          };
          this.EnrolledData.push(newPolicy);
        });
      },
      (err) => {
        console.log(err);
      }
    );
  }
  exportToExcel(data: any[], fileName: string) {
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet('My Sheet');

    // Add headers
    const headers = Object.keys(data[0]);
    worksheet.addRow(headers);

    // Add data
    data.forEach((item) => {
      console.log(item);
      const row: any = [];
      headers.forEach((header) => {
        row.push(item[header]);
      });
      worksheet.addRow(row);
    });

    worksheet.getColumn(1).width = 15;
    worksheet.getColumn(2).width = 20;

    // Generate Excel file
    workbook.xlsx.writeBuffer().then((buffer: any) => {
      const blob = new Blob([buffer], {
        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      });
      saveAs(blob, `${fileName}.xlsx`);
    });
  }

  updateBreadcrumbs() {
    let route: any = this.activatedRoute.root;
    this.breadcrumbs = [];
    let url = '';
    do {
      const childrenRoutes = route.children;
      route = null;

      childrenRoutes.forEach((childRoute: any) => {
        if (childRoute.outlet === 'primary') {
          const routeSnapshot = childRoute.snapshot;
          url += routeSnapshot.url.map((segment: any) => segment.path);
          const currentLable = routeSnapshot.data['breadcrumb'][0].label;
          if (currentLable == '' && url == '') {
            this.breadcrumbs.push({
              label: this.userRole,
              url: url,
            });
          } else {
            this.breadcrumbs.push({
              label: currentLable,
              url: url,
            });
          }
          route = childRoute;
        }
      });
    } while (route);
  }

  logOut() {
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }

  createMessage(type: string, message: string): void {
    this.message.create(type, message);
  }
}
