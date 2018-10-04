import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from 'src/guards/authentication.guard';

import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

const routes: Routes = [
    {
        path: 'home',
        children: [
            { path: '', component: HomeComponent, canActivate: [AuthenticationGuard] },
            { path: 'sign-in', component: SignInComponent }
        ]
    },
];

@NgModule({
    declarations: [
        HomeComponent,
        SignInComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        RouterModule.forRoot(routes, { useHash: true })
    ],
    providers: [],
    bootstrap: [HomeComponent]
})
export class HomeModule { }
