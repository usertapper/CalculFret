import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class IleService {
  getList() {
    throw new Error('Method not implemented.');
  }
  baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:5001";
  }

  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/iles`);
  }

  getById(ileId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/iles/${ileId}`);
  }

  

}
