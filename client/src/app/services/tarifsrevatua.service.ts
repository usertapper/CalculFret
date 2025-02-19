import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';


@Injectable({ 
  providedIn: 'root' 
})

export class TarifsRevatuaService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;


  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tarifsrevatuas`);
  }

}
