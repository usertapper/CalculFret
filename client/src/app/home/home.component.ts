import { IleService } from '../service/ile';
import { Component, inject, OnInit, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarifsRevatuaService } from '../service/tarifsrevatua';
import { TarifFretService } from '../service/tariffret';





@Component({
  selector: 'app-home',
  imports: [CommonModule,FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);
  title = 'Calcul du fret';
  iles: any[] = [];
  tarifsrevatuas : any[] = [];

  ileDepartId?: number;
  ileArriveeId?: number;
  codeTarif?: string;

  montant?: number;
  errorMessage: string = "";

  poids!: number;
  resultat!: number;
 
  ileService = new IleService(this.http);
  tarifsrevatuaService = new TarifsRevatuaService(this.http);
  tariffretService = new TarifFretService(this.http);
 
  ngOnInit(): void {
 
    this.ileService.get().subscribe((data) => {
      this.iles = data;
    });

    this.tarifsrevatuaService.get().subscribe((data) => {
      this.tarifsrevatuas = data;
    });
  }

  onSubmit(): void {
    if (this.ileDepartId && this.ileArriveeId && this.codeTarif) {
      this.getMontantFret(this.codeTarif, this.ileDepartId, this.ileArriveeId);
    } else {
      this.errorMessage = "Veuillez remplir les champs manquants"
      this.montant = undefined;
    }
  }

  getMontantFret(codeTarif: string, ileDepartId: number, ileArriveeId: number): void {
    this.tariffretService.getMontantFret(codeTarif, ileDepartId, ileArriveeId).subscribe(
      (data) => {
        this.montant = data;
        this.errorMessage = "";
      },
      (error) => {
        this.montant = undefined;
        this.errorMessage = "Aucun montant trouvé pour ces paramètres.";
        console.error("Erreur:", error);
      }
    );
  }

  calcul(): void {
    if (this.montant && this.poids) {
      this.resultat = this.poids * this.montant;
    } else {
      console.error("Le montant n'est pas définit");
    }
  }

  resetForm(): void {
    this.ileDepartId = undefined;
    this.ileArriveeId = undefined;
    this.codeTarif = undefined;
    this.montant = undefined;
    this.errorMessage = "";
  }

}
 
