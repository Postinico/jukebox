import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cabecalho',
  templateUrl: './cabecalho.component.html',
  styleUrls: ['./cabecalho.component.css']
})
export class CabecalhoComponent implements OnInit {

  usuarioNome: {} | undefined;

  saudacoes: string = '';
  saudacoesIcon: string = '';

  constructor() { }

  ngOnInit(): void {

    this.usuarioNome = localStorage.getItem('usuarioNome') || {};

    this.gerarSaudacoes();
  }

  gerarSaudacoes() {

    const hora = new Date().getHours();

    if (hora >= 6 && hora <= 11) {

      this.saudacoes = 'BOM DIA';
      this.saudacoesIcon ='sunny'

    }
    else {

      if (hora >= 12 && hora <= 17) {

        this.saudacoes = 'BOA TARDE';
        this.saudacoesIcon ='sunny'

      }
      else {

        this.saudacoes = 'BOA NOITE';
        this.saudacoesIcon ='bedtime';

      }

    }
  }

}
