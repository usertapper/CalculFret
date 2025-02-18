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

  calculMontantFret(methode: string, montant: number, poids: number, volume: number, quantite: number): number
  {
      let baseCalcule: number;

      switch(methode) {
          case "Poids":
              baseCalcule = poids;
              break;
          case "Volume":
              baseCalcule = volume;
              break;
          case "PoidsVolume":
              baseCalcule = Math.max(poids, volume);
              break;
          case "Quantité":
              baseCalcule = quantite;
              break;
          default:
              throw new Error(`Calcul du fret par ${methode} non traité`);
      }

      const result = Math.round(montant * baseCalcule);

      // Je commence par dire que c'est pas encore implémenté.
      return result < 609 ? 609 : result;
  }


}
