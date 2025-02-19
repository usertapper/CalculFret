import { IleService } from '../services/ile.service';
import { Component, inject, OnInit, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TarifsRevatuaService } from '../services/tarifsrevatua.service';
import { TarifFretService } from '../services/tariffret.service';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl } from '@angular/forms';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-quartz.css';



@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    FormsModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './home.component.html',
  template: `
      <ag-grid-angular
          class="ag-theme-quartz" style="height: 500px"
          [rowData]="rowData" [columnDefs]="colDefs" />
    `,
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
  methode?: number;
  baseCalcule?: number;
  poids?: number;
  volume?: number;
  quantite?: number;
  result?: number;

  errorMessage: string = "";
 



  ileDepartControl = new FormControl('');
  ileArriveeControl = new FormControl('');

  filteredIlesDepart: any[] = [];
  filteredIlesArrivee: any[] = [];
 
  ileService = new IleService;
  tarifsrevatuaService = new TarifsRevatuaService;
  tariffretService = new TarifFretService;

 
 
  ngOnInit(): void {
 
    this.ileService.get().subscribe((data) => {
      this.iles = data;
      this.filteredIlesDepart = this.iles;
      this.filteredIlesArrivee = this.iles;
    });

    this.tarifsrevatuaService.get().subscribe((data) => {
      this.tarifsrevatuas = data;
    });

    this.ileDepartControl.valueChanges.subscribe(value => {
      this.filteredIlesDepart = this.filterIles(value ?? '');
    });

    this.ileArriveeControl.valueChanges.subscribe(value => {
      this.filteredIlesArrivee = this.filterIles(value ?? '');
    });

    this.tariffretService;

  }

  private filterIles(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.iles.filter(ile => ile.intitule.toLowerCase().includes(filterValue));
  }

  setIleDepart(ile : any): void {
    this.ileDepartId = ile.id;
    this.ileDepartControl.setValue(ile.intitule);
  }

  setIleArrivee(ile : any): void {
    this.ileArriveeId = ile.id;
    this.ileArriveeControl.setValue(ile.intitule);
  }

  onSubmit(): void {
    if (this.ileDepartId && this.ileArriveeId && this.codeTarif ) {
      this.getTariftFret(this.codeTarif, this.ileDepartId, this.ileArriveeId,);
    } else {
      this.errorMessage = "Merci de remplir tous les champs";
      this.montant = undefined;
    }
  }

  getTariftFret(codeTarif: string, ileDepartId: number, ileArriveeId: number): void {
    this.tariffretService.getTarifFret(codeTarif, ileDepartId, ileArriveeId).subscribe(
      (data) => {
        this.montant = data.montant;
        this.errorMessage = "";
      },
      (error) => {
        this.montant = undefined;
        this.errorMessage = "Aucun montant trouvé pour ces paramètres.";
        console.error("Erreur:", error);
      }
    );
  }



  // calculMontantFret(methode: string, montant: number, poids: number, volume: number, quantite: number): void{
  //   this.result = this.tariffretService.calculMontantFret(methode, montant, poids, volume, quantite);
  // }





  resetForm(): void {
    this.ileDepartId = undefined;
    this.ileArriveeId = undefined;
    this.codeTarif = undefined;
    this.montant = undefined;
    this.errorMessage = "";
    this.poids = 0;
    this.volume = 0;
    this.result = 0;
    this.ileDepartControl.setValue('');
    this.ileArriveeControl.setValue('');
  }

}
 
