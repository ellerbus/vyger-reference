import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from './guards/authentication.guard';
import { HomePageComponent } from './pages/home/home.page';
import { DataPageComponent } from './pages/data/data.page';
import { SignInPageComponent } from './pages/signin/signin.page';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthenticationGuard] },
  { path: 'signin', component: SignInPageComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'data', component: DataPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
