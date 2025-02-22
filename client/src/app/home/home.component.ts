import { IleDto, IleService } from '../services/ile.service';
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
import { ChangeDetectorRef } from '@angular/core';



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
  poids?: number;
  volume?: number;
  quantite?: number;
  result?: number;

  errorMessage: string = "";


  ileDepartControl = new FormControl('');
  ileArriveeControl = new FormControl('');

  filteredIlesDepart: any[] = [];
  filteredIlesArrivee: any[] = [];
  codesTarifFiltres: any[] = [];
 
  constructor(
    private ileService : IleService,
    private tarifsrevatuaService : TarifsRevatuaService,
    private tariffretService : TarifFretService,
    private cdr: ChangeDetectorRef,

  ) {}
 

 
 
  ngOnInit(): void {
    this.ileService.get().subscribe(
      (data: IleDto[]) => {
      this.iles = data;
      this.filteredIlesDepart = this.iles;
      this.filteredIlesArrivee = this.iles;
      },
      (error) => {
        console.error('Error fetching iles:', error);
      }
    );

    this.tarifsrevatuaService.get().subscribe(
      (data) => {
        this.tarifsrevatuas = data;
      },
      (error) => {
        console.error('Error fetching tarifs:', error);
      }
    );


    this.ileDepartControl.valueChanges.subscribe(value => {
      this.filteredIlesDepart = this.filterIles(value ?? '');
    });

    this.ileArriveeControl.valueChanges.subscribe(value => {
      this.filteredIlesArrivee = this.filterIles(value ?? '');
    });

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

  onChange(): void {
      if (this.ileDepartId && this.ileArriveeId && this.codeTarif) {
        this.getTarifFret(this.codeTarif, this.ileDepartId, this.ileArriveeId);
      }
    }
    
  onSubmit(): void {
    if (this.ileDepartId && this.ileArriveeId && this.codeTarif ) {
      this.getTarifFret(this.codeTarif, this.ileDepartId, this.ileArriveeId,);
    } else {
      this.errorMessage = "Merci de remplir tous les champs";
      this.montant = undefined;
    }
  }

  getTarifFret(codeTarif: string, ileDepartId: number, ileArriveeId: number): void {
  
    this.tariffretService.getTarifFret(codeTarif, ileDepartId, ileArriveeId).subscribe(
      (data) => {
        this.montant = data?.montant ?? undefined;
        this.errorMessage = "";
        console.log("Appel de l'API (getTarifFret):", data);
      },
      (error) => {
        this.montant = undefined;
        console.error("Erreur API :");
  
        if (error.status === 404) {
          this.errorMessage = "Aucun montant trouvé pour ces paramètres.";
        } else {
          this.errorMessage = `Erreur (${error.status}):`;
        }
      }
    );
  }


  calculMontantFret(codeTarif: string, ileDepartId: number, ileArriveeId: number, poids: number, volume: number, quantite: number): void {
    console.log("API: ", {codeTarif, ileDepartId, ileArriveeId, poids, volume, quantite});
  
      this.tariffretService.calculMontantFret(codeTarif, ileDepartId, ileArriveeId, poids, volume, quantite).subscribe(
        (data) => {
          console.log(data);
          this.result = data ?? undefined ;
          this.errorMessage = "";
        },
        (error) => {
          console.error(error);
          this.result = undefined;
          this.errorMessage = `Erreur de calcul du fret (${error.status}:)`;

        }
      );
  }



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
 
