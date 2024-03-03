// import { Menu } from '../../models/menu';
// import { navigationAnimation } from '../../animations';
import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterOutlet,
  NavigationEnd,
} from '@angular/router';
import { NzBreadCrumbComponent } from 'ng-zorro-antd/breadcrumb';

import { filter } from 'rxjs';

@Component({
  selector: 'app-base-layout',
  templateUrl: './base-layout.component.html',
  styleUrls: ['./base-layout.component.scss'],
  // animations: [navigationAnimation],
})
export class BaseLayoutComponent implements OnInit {
  breadcrumbs: any[] = [];
  isCollapsed = false;

  toggleCollapsed(): void {
    this.isCollapsed = !this.isCollapsed;
  }
  menu: {
    title: string;
    icon: string;
    path?: string;
    // children?: Menu[];
  }[] = [];

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.setBreadcrumbs();
      });
    // if (this.user.role === 'MENTOR')
    this.menu = [
      {
        title: 'Home',
        icon: 'home',
        path: 'home',
      },
      // {
      //   title: 'Day Attendance',
      //   icon: 'team',
      //   path: 'day-attendance',
      // },
      // {
      //   title: "Hourly Attendance",
      //   icon: "houricon",
      //   path: "hourly-attendance",
      // },
    ];
    // else
    //   this.menu = [
    //     {
    //       title: 'Home',
    //       icon: 'home',
    //       path: 'home',
    //     },
    //     {
    //       title: 'Mentor',
    //       icon: 'crown',
    //       path: 'mentor',
    //     },
    //     {
    //       title: 'Students',
    //       icon: 'user',
    //       path: 'students',
    //     },
    //     {
    //       title: 'Day Attendance',
    //       icon: 'team',
    //       path: 'day-attendance',
    //     },

    //     {
    //       title: 'Time Table',
    //       icon: 'timetable',
    //       path: 'time-tables',
    //     },
    //     {
    //       title: 'Section',
    //       icon: 'contacts',
    //       path: 'sections',
    //     },
    //     {
    //       title: 'Subject',
    //       icon: 'book',
    //       path: 'subjects',
    //     },
    //     {
    //       title: 'Total Hours',
    //       icon: 'clock',
    //       path: 'total-hours',
    //     },
    //   ];
  }

  prepareRoute(outlet: RouterOutlet) {
    return (
      outlet &&
      outlet.activatedRouteData &&
      outlet.activatedRouteData['animationState']
    );
  }

  setBreadcrumbs(): void {
    const route = this.activatedRoute.snapshot;
    this.breadcrumbs = route.data['breadcrumb'] || [];
  }

  logOut() {
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }
}
