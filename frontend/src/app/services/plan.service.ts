import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { plan } from '../model/plan';
import { GenericService } from './generic-service.service';

@Injectable({
  providedIn: 'root',
})
export class PlanService extends GenericService<plan> {
  constructor(private http: HttpClient) {
    super(http, 'api/plan');
  }
}
