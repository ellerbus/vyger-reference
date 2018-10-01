import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { HomeRouterModule } from './home-router.module';
import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

@NgModule({
    declarations: [
        HomeComponent,
        SignInComponent,
    ],
    imports: [
        BrowserModule,
        RouterModule,
        HomeRouterModule
    ],
    providers: [],
    bootstrap: [HomeComponent]
})
export class HomeModule { }
