import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CalculService {
  calculFret(poids: number, volume: number, montant: number): number | string {
    if (!poids || !volume || !montant) {
      return 'Le montant ou le poids ou le volume est invalide';
    }
    
    const lastPoids = poids;
    const lastVolume = volume;

    // Mémoriser les derniers poids et volume si nécessaire
    // Si besoin, on peut stocker lastPoids et lastVolume dans un service ou un store.

    return Math.max(poids, volume) * montant;
  }
}
