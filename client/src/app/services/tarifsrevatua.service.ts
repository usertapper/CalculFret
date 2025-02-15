import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class TarifsRevatuaService {
  getList() {
    throw new Error('Method not implemented.');
  }
  baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:5001";
  }

  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tarifsrevatuas`);
  }

  getById(tarifsrevatuasId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tarifsrevatuas/${tarifsrevatuasId}`);
  }


}
