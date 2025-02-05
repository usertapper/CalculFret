
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";

@Component({
  selector: 'app-root',
  imports: [HomeComponent, RegisterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  http = inject(HttpClient);
  title = 'Calcul du fret';
  iles: any;

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/iles').subscribe({
      next: response => this.iles = response,
      error: error => console.log(error),
      complete: ()=> console.log('Request has completed')
    })
  }
}
