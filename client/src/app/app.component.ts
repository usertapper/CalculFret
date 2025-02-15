
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { HomeComponent } from "./home/home.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  http = inject(HttpClient);
  title = 'Calcul du fret';
  iles: any;

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/tariffret').subscribe({
      next: response => this.iles = response,
      error: error => console.log(error),
      complete: ()=> console.log('Request has completed')
    })
  }
}
