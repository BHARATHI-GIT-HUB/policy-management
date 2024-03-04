import { Injectable } from '@angular/core';
import { GenericService } from './generic-service.service';
import { Provider } from '../model/provider';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProviderService extends GenericService<Provider> {
  constructor(private http: HttpClient) {
    super(http, 'api/provider');
  }
}
