import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';

export interface TarifFretDto {
  tariffret: number | undefined;
  code: string;
  methode: string;
  montant: number;
}

@Injectable({ 
  providedIn: 'root' 
})

export class TarifFretService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  get(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret`)
  }

  getMontants(): Observable<number[]> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret/montants`);
  }


  getCodeTarifs(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/tariffret/codes`);
  }


  getTarifFret(codeTarif: string, ileDepartId: number, ileArriveeId: number): Observable<TarifFretDto> {

    const params = new HttpParams()
    .set('codeTarif', codeTarif)
    .set('ileDepartId', ileDepartId.toString())
    .set('ileArriveeId', ileArriveeId.toString());

    return this.http.get<TarifFretDto>(`${this.baseUrl}/api/tariffret/tarif/`, {params})
  }

  calculMontantFret(codeTarif: string, ileDepartId: number, ileArriveeId: number, poids: number, volume: number, quantite: number): Observable<number> {

    

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
        throw new Error('Calcul du fret par {t.Methode} non traité');
    }

    const result = Math.Round

    const params = new HttpParams()
    .set('codeTarif', codeTarif)
    .set('ileDepartId', ileDepartId.toString())
    .set('ileArriveeId', ileArriveeId.toString())
    .set('poids', poids)
    .set('volume', volume)
    .set('quantite', quantite)
    return this.http.get<number>(`${this.baseUrl}/api/tariffret/calcul/`, {params})

  }


























  // calculMontantFret(methode: string, montant: number, poids: number, volume: number, quantite: number): number
  // {
  //     let baseCalcule: number;

  //     switch(methode) {
  //         case "Poids":
  //             baseCalcule = poids;
  //             break;
  //         case "Volume":
  //             baseCalcule = volume;
  //             break;
  //         case "PoidsVolume":
  //             baseCalcule = Math.max(poids, volume);
  //             break;
  //         case "Quantité":
  //             baseCalcule = quantite;
  //             break;
  //         default:
  //             throw new Error(`Calcul du fret par ${methode} non traité`);
  //     }

  //     const result = Math.round(montant * baseCalcule);

  //     // Je commence par dire que c'est pas encore implémenté.
  //     return result < 609 ? 609 : result;
  // }


}
