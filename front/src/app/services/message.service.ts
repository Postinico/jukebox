import { Injectable, OnInit } from '@angular/core';
import Swal from 'sweetalert2/dist/sweetalert2.js';


@Injectable({
  providedIn: 'root'
})
export class MessageService implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public mensagemErro(mensagem: string): void {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: mensagem,
    })
  }

  public mensagemSucesso(mensagem: string): void {
    Swal.fire({
      icon: 'success',
      title: 'Bom trabalho!',
      text: mensagem,
    })
  }
}
