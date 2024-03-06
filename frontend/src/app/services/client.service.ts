import { Injectable } from '@angular/core';
import { GenericService } from './generic-service.service';
import { Client } from '../model/client';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ClientService extends GenericService<Client> {
  constructor(private http: HttpClient) {
    super(http, 'api/client');
  }
}
