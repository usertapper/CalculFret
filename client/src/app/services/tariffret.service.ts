import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class TarifFretService implements OnInit{
  getList() {
    throw new Error('Method not implemented.');
  }
  baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:5001";
  }
    ngOnInit(): void {
        throw new Error('Method not implemented.');
    }

  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret`);
  }

  getMontants(): Observable<number[]> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret/montants`);
  }

  getMontantFret(codeTarif: string, ileDepartId: number, ileArriveeId: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/api/tariffret/montant`, {
        params: {
            codeTarif: codeTarif,
            ileDepartId: ileDepartId.toString(),
            ileArriveeId: ileArriveeId.toString()
        }
    });
  }

  getCodeTarifs(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret/codetarifs`);
  }


}
