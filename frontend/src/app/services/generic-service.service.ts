import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export abstract class GenericService<T> {
  private baseUrl = environment.apiurl;
  constructor(private httpClient: HttpClient, private apiUrl: string) {}

  public create(resource: Partial<T>): Observable<T> {
    return this.httpClient.post<T>(`${this.baseUrl}${this.apiUrl}`, resource);
  }

  public get(): Observable<T[]> {
    return this.httpClient.get<T[]>(`${this.baseUrl}${this.apiUrl}`);
  }

  public getAll(): Observable<T[]> {
    return this.httpClient.get<T[]>(`${this.baseUrl}${this.apiUrl}`);
  }
  public getById(id: number): Observable<T> {
    return this.httpClient.get<T>(`${this.baseUrl}${this.apiUrl}/${id}`);
  }

  public update(resource: Partial<T>, id: number): Observable<T> {
    return this.httpClient.put<T>(
      `${this.baseUrl}${this.apiUrl}/${id}`,
      resource
    );
  }

  public delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}${this.apiUrl}/${id}`);
  }
}
