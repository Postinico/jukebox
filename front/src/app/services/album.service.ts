import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry,catchError } from 'rxjs/operators';
import { Album } from '../models/Album.Model';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  url = 'https://localhost:44389/api/albuns/obter-generoid/';

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('token')}`  })
  }

  obterAlbuns(generoid:number): Observable<Album[]> {
    console.log(localStorage.getItem('token'));
    return this.httpClient.get<Album[]>(this.url+generoid,this.httpOptions)
      .pipe(
        retry(2))
  }

   handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };
}
