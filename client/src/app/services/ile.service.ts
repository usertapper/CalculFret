import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment.development';

export interface IleDto {
  Id : number;
  Intitule : string;
}

@Injectable({ 
  providedIn: 'root' 
})

export class IleService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  

  get(): Observable<IleDto[]> {
    return this.http.get<IleDto[]>(`${this.baseUrl}/api/iles`);
  }

  getById(ileId: number): Observable<IleDto[]> {
    return this.http.get<IleDto[]>(`${this.baseUrl}/api/iles/${ileId}`);
  }

  

}
