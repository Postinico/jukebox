import { Component, OnInit } from '@angular/core';
import { GeneroViewModel } from 'src/app/models/ViewModels/GeneroViewModel';
import { GeneroService } from 'src/app/services/genero.service';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {

  public generos: GeneroViewModel[] = [];

  constructor(private _generoServico: GeneroService) { }

  ngOnInit(): void {

    this.obterGeneros();
    
  }

  obterGeneros() {
    this._generoServico.obterGeneros().subscribe((Generos: GeneroViewModel[]) => {
      this.generos = Generos;  console.log(this.generos);
    });
  }

}
