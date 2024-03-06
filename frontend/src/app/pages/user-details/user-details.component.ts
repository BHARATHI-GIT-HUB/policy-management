import { user } from './../../model/user';
import { PolicyService } from './../../services/policy.service';
import { AgentService } from './../../services/agent.service';
import { ClientService } from './../../services/client.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client, Provider, Agent } from '../../model';
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
  providerData: Provider[] = [];
  providerHeader: string[] = [];
  agentData: Agent[] = [];
  agentHeader: string[] = [];
  responseMessage: string = '';
  errorMessage: string = '';
  originalClientData: Client[] = [];
  originalAgentData: Agent[] = [];
  originalProviderData: Provider[] = [];
  // this.originalClientData = data;
  // this.originalClientData = data;

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private providerService: ProviderService,
    private agentService: AgentService
  ) {}

  getAllClient() {
    this.clientService.getAll().subscribe(
      (data: Client[]) => {
        data.forEach((element: Client) => {
          let newClient: any = {
            id: element.id,
            name: element.user ? element.user.username : '',
            Email: element.mailId,
            Mobile: element.mobileNo,
            Address: element.street,
          };
          this.clientData.push(newClient);
        });

        this.originalClientData = data;
      },
      (err) => console.log(err)
    );
  }

  getAllProvider() {
    this.providerService.getAll().subscribe(
      (data: Provider[]) => {
        data.forEach((element: Provider) => {
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
        console.log(data, 'provider');
        this.originalProviderData = data;
      },
      (err) => console.log(err)
    );
  }

  getAllAgent() {
    this.agentService.getAll().subscribe(
      (data: Agent[]) => {
        data.forEach((element: Agent) => {
          let newAgent: any = {
            id: element.id,
            Name: element.user.username,
            Qualification: element.qualification,
            Mobile: element.mobileNo,
            Address: element.street + ',' + element.city.name,
          };
          this.agentData.push(newAgent);
        });
        this.originalAgentData = data;
      },
      (err) => console.log(err)
    );
  }

  onDeleteClient(id: number) {
    this.clientService.delete(id).subscribe(
      (res: any) => {
        console.log(res);
        this.responseMessage = res.message;
        this.getAllClient();
      },
      (err) => {
        console.log(err);
        this.errorMessage = err.message;
      }
    );
    this.getAllClient();
  }
  onDeleteProvider(id: number) {
    // this.providerService.delete(id).subscribe(
    //   (res: any) => {
    //     this.responseMessage = res.message;
    //     this.getAllClient();
    //   },
    //   (err) => (this.errorMessage = err.message)
    // );
  }
  onDeleteAgent(id: number) {
    // this.agentService.delete(id).subscribe(
    //   (res: any) => {
    //     this.responseMessage = res.message;
    //     this.getAllClient();
    //   },
    //   (err) => (this.errorMessage = err.message)
    // );
  }

  onUpdateClient(updatedVlaue: any) {
    const result = <Client>(
      this.originalClientData.find((item) => item.id === updatedVlaue.id)
    );

    const bodyData = {
      id: updatedVlaue.id,
      dob: result.dob,
      mobileNo: updatedVlaue.Mobile,
      mailId: updatedVlaue.Email,
      fatherName: result.fatherName,
      motherName: result.motherName,
      nationality: result.nationality,
      street: updatedVlaue.Address,
      cityId: result.cityId,
      userId: result.userId,
    };
    console.log(updatedVlaue.id, result.id, result);

    this.clientService.update(bodyData, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        console.log(res, 'response');
        this.getAllClient();
      },
      (err) => {
        console.log(err);
        this.errorMessage = err.message;
        if (this.errorMessage == ' ') {
          this.errorMessage = err.title;
        }
      }
    );
  }
  onUpdateProvider(updatedVlaue: any) {
    const result = <Provider>(
      this.originalProviderData.find((item) => item.id === updatedVlaue.id)
    );
    const bodyData = {
      id: updatedVlaue.id,
      phoneNo: result.phoneNo,
      moblieNo: updatedVlaue.Mobile,
      mailId: updatedVlaue.Email,
      cityId: result.cityId,
      street: updatedVlaue.Address,
      launchDate: result.launchDate,
      testimonials: result.testimonials,
      description: result.description,
      companyName: result.companyName,
      userId: result.userId,
    };

    this.providerService.update(bodyData, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllProvider();
      },
      (err) => {
        console.log(err);
        this.errorMessage = err.message;
      }
    );
  }
  onUpdateAgent(updatedVlaue: any) {
    const result = <Agent>(
      this.originalAgentData.find((item) => item.id === updatedVlaue.id)
    );

    const bodyData = {
      id: updatedVlaue.id,
      dob: result.dob,
      mobileNo: updatedVlaue.Mobile,
      cityId: result.cityId,
      street: updatedVlaue.Address,
      qualification: result.qualification,
      aadharNo: result.aadharNo,
      panNo: result.panNo,
      userId: result.userId,
    };
    this.agentService.update(bodyData, updatedVlaue.id).subscribe(
      (res: any) => {
        this.responseMessage = res.message;
        this.getAllAgent();
      },
      (err) => {
        console.log(err);
        this.errorMessage = err.message;
      }
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
