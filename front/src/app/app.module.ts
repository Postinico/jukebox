import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AutenticarComponent } from './pages/autenticar/autenticar.component';
import { CabecalhoComponent } from './pages/cabecalho/cabecalho.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RodapeComponent } from './pages/rodape/rodape.component';
import { PrincipalComponent } from './pages/principal/principal.component';
import { HttpClientModule } from '@angular/common/http';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { LoadingComponent } from './pages/loading/loading.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AlbumComponent } from './pages/album/album.component';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [
    AppComponent,
    AutenticarComponent,
    CabecalhoComponent,
    RodapeComponent,
    PrincipalComponent,
    LoadingComponent,
    AlbumComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    MatIconModule,
    CommonModule,
    HttpClientModule,
    MatButtonModule,
    MatDialogModule,
    FormsModule,
    MatProgressBarModule,
    SweetAlert2Module,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
