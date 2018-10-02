import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRouterModule } from './app-router.module';
import { SideMenuComponent } from '../side-menu/side-menu.component';
import { PageTitleComponent } from '../page-title/page-title.component';
import { PageHeaderComponent } from '../page-header/page-header.component';

import { HomeModule } from '../home/home.module';
import { AuthenticationService } from '../services/authentication.service';
import { AuthenticationGuard } from '../guards/authentication.guard';
import { ExercisesModule } from '../exercises/exercises.module';
import { DataGuard } from '../guards/data.guard';

export function initializeGoogleApi(authenticationService: AuthenticationService) {
    return () => authenticationService.initializeClient();
}

@NgModule({
    declarations: [
        AppComponent,
        SideMenuComponent,
        PageTitleComponent,
        PageHeaderComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        AppRouterModule,
        HomeModule,
        ExercisesModule
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: initializeGoogleApi, deps: [AuthenticationService], multi: true },
        AuthenticationGuard,
        DataGuard
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
