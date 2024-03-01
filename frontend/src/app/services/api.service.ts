import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService<T> {
  private baseUrl = environment.apiurl;

  constructor(private httpClient: HttpClient) {}

  public create(apiUrl: string, resource: T): Observable<T> {
    return this.httpClient.post<T>(`${this.baseUrl}/${apiUrl}`, resource);
  }

  public getById(apiUrl: string, id: number): Observable<T> {
    return this.httpClient.get<T>(`${this.baseUrl}/${apiUrl}/${id}`);
  }

  public getAll(apiUrl: string): Observable<T[]> {
    return this.httpClient.get<T[]>(`${this.baseUrl}/${apiUrl}`);
  }

  public update(apiUrl: string, id: number, resource: T): Observable<T> {
    return this.httpClient.put<T>(`${this.baseUrl}/${apiUrl}/${id}`, resource);
  }

  public delete(apiUrl: string, id: number): Observable<T> {
    return this.httpClient.delete<T>(`${this.baseUrl}/${apiUrl}/${id}`);
  }
}
