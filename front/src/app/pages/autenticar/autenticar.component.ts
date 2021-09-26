import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PostAutenticar } from 'src/app/models/InputModels/PostAutenticarInputModel';
import { AutenticarViewModel } from 'src/app/models/ViewModels/AutenticarViewModel';
import { MatDialog } from '@angular/material/dialog';
import { AutenticarService } from './autenticar.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'autenticar',
  templateUrl: './autenticar.component.html',
  styleUrls: ['./autenticar.component.css']
})
export class AutenticarComponent implements OnInit {

  autenticar: PostAutenticar = { email: '', senha: '' };
  email: string = '';
  senha: string = '';
  carregando: boolean = false;

  acesso: AutenticarViewModel = { token: '', usuario: { email: '', funcao: '', nome: '', perfilId: '', senha: '', usuarioId: '' } };

  constructor(
    private router: Router,
    private _autenticarService: AutenticarService,
    public dialog: MatDialog,
    private _msgService: MessageService
  ) { }

  ngOnInit(): void {
  }

  login() {
    this.carregando = true;
    this.autenticar.email = this.email;
    this.autenticar.senha = this.senha;

    console.log(this.autenticar);
    this.router.navigate(['/main/']);
    
    this._autenticarService.login(this.autenticar).subscribe(
      sucesso => { this.entrar(sucesso); console.log(sucesso) },
      falha => { this.entrar(falha); console.log(falha) }
    );
  }

  entrar(loginOn: AutenticarViewModel) {
   
    if (loginOn.token != null) {

      localStorage.setItem('token', loginOn.token);

      localStorage.setItem('usuario', loginOn.usuario.nome);

      this.router.navigate(['/main/']);

    }
    else {

      console.log('erro login');

      this.carregando = false;

      this._msgService.mensagemErro('usuário ou senha não existe!');

    }

  }
}


