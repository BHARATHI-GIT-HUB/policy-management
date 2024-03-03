import { PolicyService } from './../../services/policy.service';
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
  listOfClientColumn: any[] = [];
  listOfAgentColumn: any[] = [];
  listOfProviderColumn: any[] = [];
  providerData: provider[] = [];
  providerHeader: string[] = [];
  agentData: Agent[] = [];
  agentHeader: string[] = [];
  responseMessage: string = '';
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private providerService: ProviderService,
    private agentService: AgentService
  ) {}

  getAllClient() {
    this.clientService.getAll().subscribe(
      (data: any) => {
        console.log('client data :', data);
        data.forEach((element: any) => {
          let newClient: any = {
            id: element.id,
            Name: element.user.username,
            Email: element.mailId,
            Mobile: element.mobileNo,
            Address: element.street + ',' + element.city.name,
          };
          this.clientData.push(newClient);
        });
      },
      (err) => console.log(err)
    );
  }

  getAllProvider() {
    this.providerService.getAll().subscribe(
      (data: any) => {
        data.forEach((element: any) => {
          let newProvider: any = {
            id: element.id,
            Name: element.companyName,
            Description: element.description,
            Email: element.mailId,
            Mobile: element.moblieNo,
            Address: element.street + ',' + element.city.name,
          };
          this.providerData.push(newProvider);
        });
      },
      (err) => console.log(err)
    );
  }

  getAllAgent() {
    this.agentService.getAll().subscribe(
      (data: any) => {
        data.forEach((element: any) => {
          console.log(element);
          let newAgent: any = {
            id: element.id,
            Name: element.user.username,
            Qualification: element.qualification,
            Mobile: element.mobileNo,
            Address: element.street + ',' + element.city.name,
          };
          this.agentData.push(newAgent);
        });
      },
      (err) => console.log(err)
    );
  }

  onDeleteClient(id: number) {
    this.clientService.delete(id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllClient();
      },
      (err) => (this.errorMessage = err.message)
    );
  }
  onDeleteProvider(id: number) {
    this.providerService.delete(id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllClient();
      },
      (err) => (this.errorMessage = err.message)
    );
  }
  onDeleteAgent(id: number) {
    this.agentService.delete(id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllClient();
      },
      (err) => (this.errorMessage = err.message)
    );
  }

  onUpdateClient(updatedVlaue: any) {
    const databody: any = {
      email: updatedVlaue.Email,
      mobile: updatedVlaue.Mobile,
      street: updatedVlaue.Address,
    };

    this.clientService.update(databody, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllClient();
      },
      (err) => (this.errorMessage = err.message)
    );
  }
  onUpdateProvider(updatedVlaue: any) {
    const databody: any = {
      email: updatedVlaue.Email,
      mobile: updatedVlaue.Mobile,
      street: updatedVlaue.Address,
    };

    this.providerService.update(databody, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllProvider();
      },
      (err) => (this.errorMessage = err.message)
    );
  }
  onUpdateAgent(updatedVlaue: any) {
    const databody: any = {
      mobile: updatedVlaue.Mobile,
      street: updatedVlaue.Address,
    };

    this.agentService.update(databody, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllAgent();
      },
      (err) => (this.errorMessage = err.message)
    );
  }

  ngOnInit(): void {
    this.listOfClientColumn = [
      {
        title: 'Name',
      },
      {
        title: 'Email',
      },
      {
        title: 'Mobile',
      },
      {
        title: 'Address',
      },
      {
        title: 'Action',
      },
    ];
    this.listOfAgentColumn = [
      {
        title: 'Name',
      },

      {
        title: 'Qualification',
      },
      {
        title: 'Mobile',
      },
      {
        title: 'Address',
      },
      {
        title: 'Action',
      },
    ];
    this.listOfProviderColumn = [
      {
        title: 'Name',
      },
      {
        title: 'Description',
      },
      {
        title: 'Email',
      },
      {
        title: 'Mobile',
      },
      {
        title: 'Address',
      },
      {
        title: 'Action',
      },
    ];

    this.breadcrumbName = this.route.snapshot.data['title'];
    this.getAllClient();
    this.getAllProvider();
    this.getAllAgent();
  }
}
