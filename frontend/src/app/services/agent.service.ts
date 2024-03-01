import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GenericService } from './generic-service.service';
import { Agent } from '../model';
@Injectable({
  providedIn: 'root',
})
export class AgentService extends GenericService<Agent> {
  constructor(private http: HttpClient) {
    super(http, '/agent');
  }
}
