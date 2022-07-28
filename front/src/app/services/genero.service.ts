import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';
import { GeneroViewModel } from '../models/ViewModels/GeneroViewModel';

@Injectable({
  providedIn: 'root'
})
export class GeneroService {

  url = 'https://localhost:44389/api/generos'; // api rest fake

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('token')}` })
  }

  obterGeneros(): Observable<GeneroViewModel[]> {
    return this.httpClient.get<GeneroViewModel[]>(this.url, this.httpOptions)
      .pipe(
        retry(2))
  }

}
