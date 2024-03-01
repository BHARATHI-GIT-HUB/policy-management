import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GenericService } from './generic-service.service';
import { policy } from '../model';

@Injectable({
  providedIn: 'root',
})
export class PolicyService extends GenericService<policy> {
  constructor(private http: HttpClient) {
    super(http, '/policy');
  }
}
