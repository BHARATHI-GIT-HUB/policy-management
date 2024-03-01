import { Injectable } from '@angular/core';
import { GenericService } from './generic-service.service';
import { provider } from '../model/provider';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProviderService extends GenericService<provider> {
  constructor(private http: HttpClient) {
    super(http, '/provider');
  }
}
