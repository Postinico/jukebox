import { Component, OnInit } from '@angular/core';
import { Album } from 'src/app/models/Album.Model';
import { GeneroViewModel } from 'src/app/models/ViewModels/GeneroViewModel';
import { AlbumService } from 'src/app/services/album.service';
import { Router } from '@angular/router';
import { GeneroService } from 'src/app/services/genero.service';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})

export class PrincipalComponent implements OnInit {

  public generos: GeneroViewModel[] = [];
  public albuns: Album[] = [];

  genero: GeneroViewModel | undefined = { id: '', titulo: '' };

  constructor(private router: Router, private _generoServico: GeneroService, private _albumServico: AlbumService) { }

  ngOnInit(): void {
    this.obterGeneros();
  }

  obterGeneros() {
    this._generoServico.obterGeneros().subscribe(
      (
        Generos: GeneroViewModel[]) => {
        this.generos = Generos; console.log(this.generos);
      },
      erro => {
        if (erro.status == 401) {
          localStorage.clear();
          this.router.navigate(['/autenticar/']);
        }
      }
    );
  }

  generoEnter() {
    this.obterGeneros();
  }

  generoSelecionado(valor: any) {
    this.albuns = [];
    console.log(valor.value);
    this.genero = this.generos.find(g => g.id === valor.value);
    var result = this.generos.filter(function (o) { return o.titulo == valor.value; });
    const titulo = result[0].id;

    this._albumServico.obterAlbuns(Number.parseInt(result[0].id)).subscribe((a: Album[]) => {
      for (var i = 0; i < a.length; i++) {
        this.albuns.push(a[i]);
      }
    });
  }
}
