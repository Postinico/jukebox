import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AutenticarViewModel } from 'src/app/models/ViewModels/AutenticarViewModel';
import { PostAutenticar } from 'src/app/models/InputModels/PostAutenticarInputModel';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { GeneroViewModel } from '../models/ViewModels/GeneroViewModel';

@Injectable({
  providedIn: 'root'
})
export class GeneroService {

  url = 'https://localhost:44389/api/generos'; // api rest fake

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('token')}`  })
  }

  obterGeneros(): Observable<GeneroViewModel[]> {
    console.log(localStorage.getItem('token'));
    return this.httpClient.get<GeneroViewModel[]>(this.url,this.httpOptions)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };

}
