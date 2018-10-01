import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from 'src/guards/authentication.guard';

import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

const routes: Routes = [
    {
        path: 'home',
        children: [
            { path: '', component: HomeComponent, canActivate: [AuthenticationGuard] },
            { path: 'sign-in', component: SignInComponent },
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, { useHash: true })
    ],
    exports: [RouterModule]
})
export class HomeRouterModule { }
