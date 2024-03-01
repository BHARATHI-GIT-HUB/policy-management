import { AgentService } from './../../services/agent.service';
import { ClientService } from './../../services/client.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client, provider, Agent } from '../../model';
import { ApiService } from '../../services/api.service';
import { CloudFilled } from '@ant-design/icons';
import { ProviderService } from '../../services/provider.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss',
})
export class UserDetailsComponent implements OnInit {
  breadcrumbName: string = '';
  clientData: Client[] = [];
  clientHeader: string[] = [];
  providerData: provider[] = [];
  providerHeader: string[] = [];
  agentData: Agent[] = [];
  agentHeader: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private ProviderService: ProviderService,
    private agentService: AgentService
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

  getAllProvider() {
    this.ProviderService.getAll().subscribe(
      (data: provider[]) => {
        this.providerData = data;
        console.log('provider data : ' + data);
      },
      (err) => console.log(err)
    );
  }

  getAllAgent() {
    this.agentService.getAll().subscribe(
      (data: Agent[]) => {
        this.agentData = data;
        console.log('agent data : ' + data);
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
      'Father ',
      'Mother ',
      'Nationality',
      'Street',
      'City Id',
    ];
    this.providerHeader = [
      'User Id',
      'Phone No',
      'Mobile No',
      'Mail Id',
      'City Id',
      'Street',
      'Launch Date',
      'Testimonials',
      'Description',
      'Company Name',
    ];
    this.agentHeader = [
      'User Id',
      'DOB',
      'Street',
      'City Id',
      'Mobile No',
      'Qualification',
      'Aadhar No',
      'Pan No',
    ];
    this.breadcrumbName = this.route.snapshot.data['title'];
    this.getAllClient();
    this.getAllProvider();
    this.getAllAgent();
  }
}
