import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticarComponent } from './pages/autenticar/autenticar.component';
import { PrincipalComponent } from './pages/principal/principal.component';

const routes: Routes = [
  { path: 'autenticar', component: AutenticarComponent },
  { path: 'main', component: PrincipalComponent },
  { path: '', redirectTo: '/autenticar', pathMatch: 'full' },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
