import { ClientService } from './../../services/client.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from '../../model/client';
import { ApiService } from '../../services/api.service';
import { CloudFilled } from '@ant-design/icons';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss',
})
export class UserDetailsComponent implements OnInit {
  breadcrumbName: string = '';
  clientData: Client[] = [];
  clientHeader: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService
  ) {}

  getAllClient() {
    this.clientService.getAll().subscribe(
      (data: Client[]) => {
        this.clientData = data;
        console.log('client data : ' + data);
      },
      (err) => console.log(err)
    );
  }

  ngOnInit(): void {
    this.clientHeader = [
      'First Name',
      'Last Name',
      'DOB',
      'Mobile No',
      'Mail Id',
      'Father Name',
      'Mother Name',
      'Nationality',
      'Street',
      'City Id',
    ];
    this.breadcrumbName = this.route.snapshot.data['title'];
    this.getAllClient();
  }
}
