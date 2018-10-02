import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from 'src/guards/authentication.guard';

import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { DataComponent } from './components/data/data.component';

const routes: Routes = [
    {
        path: 'home',
        children: [
            { path: '', component: HomeComponent, canActivate: [AuthenticationGuard] },
            { path: 'sign-in', component: SignInComponent },
            { path: 'data', component: DataComponent, canActivate: [AuthenticationGuard] },
        ]
    },
];

@NgModule({
    declarations: [
        HomeComponent,
        SignInComponent,
        DataComponent
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
