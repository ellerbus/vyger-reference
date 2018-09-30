import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { DataGuard } from 'src/guards/data.guard';
import { HomePageComponent } from './pages/home/home.page';
import { DataPageComponent } from './pages/data/data.page';
import { SignInPageComponent } from './pages/signin/signin.page';
import { ExercisesPageComponent } from './pages/exercises/exercises.page';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthenticationGuard] },
    { path: 'signin', component: SignInPageComponent },
    { path: 'data', component: DataPageComponent },
    { path: 'home', component: HomePageComponent, canActivate: [DataGuard] },
    { path: 'exercises', component: ExercisesPageComponent, canActivate: [DataGuard] },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
