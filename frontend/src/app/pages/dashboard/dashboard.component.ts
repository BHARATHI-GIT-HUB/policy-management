import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  breadcrumbName: string = '';
  constructor(private route: ActivatedRoute) {}
  ngOnInit() {
    this.breadcrumbName = this.route.snapshot.data['title'];
  }
}
