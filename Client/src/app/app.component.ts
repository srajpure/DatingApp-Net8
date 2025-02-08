import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DatingService } from './dating.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HttpClientModule,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers : [DatingService]
  
})
export class AppComponent implements OnInit{

  title = 'Client';
  Users :any

  constructor(private datingObj :DatingService) {
  }

  ngOnInit(): void {
    this.datingObj.GetUsers().subscribe(result => 
      this.Users = result
    )
  } 
}
