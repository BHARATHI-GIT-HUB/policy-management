import { forEach } from '@angular-devkit/schematics';
// import { Menu } from '../../models/menu';
// import { navigationAnimation } from '../../animations';
import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterOutlet,
  NavigationEnd,
  Navigation,
} from '@angular/router';
import { NzBreadCrumbComponent } from 'ng-zorro-antd/breadcrumb';

import { every, filter } from 'rxjs';

@Component({
  selector: 'app-base-layout',
  templateUrl: './base-layout.component.html',
  styleUrls: ['./base-layout.component.scss'],
})
export class BaseLayoutComponent implements OnInit {
  breadcrumbs: any[] = [];
  isCollapsed = false;
  user_role: string = 'Provider';
  currentPath: string = '';
  toggleCollapsed(): void {
    this.isCollapsed = !this.isCollapsed;
  }
  menu: {
    title: string;
    icon: string;
    path?: string;
  }[] = [];

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.currentPath = event.url.slice(1);
        this.updateBreadcrumbs();
      });

    if (this.user_role === 'Admin') {
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
    } else if (this.user_role === 'Provider') {
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
    } else if (this.user_role === 'Agent') {
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
          this.breadcrumbs.push({
            label: routeSnapshot.data['breadcrumb'][0].label,
            url: url,
          });
          route = childRoute;
        }
      });
    } while (route);
  }

  logOut() {
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }
}
