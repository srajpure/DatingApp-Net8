import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DatingService {

  constructor(private httpClient : HttpClient) { }

 GetUsers()
  {
    return this.httpClient.get('https://localhost:5015/api/Users');
  }
}
