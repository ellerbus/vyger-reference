import { NgModule, APP_INITIALIZER, Directive } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SortablejsModule } from 'angular-sortablejs';

import { AppComponent } from './app.component';
import { AppRouterModule } from './app-router.module';
import { SideMenuComponent } from '../side-menu/side-menu.component';
import { PageTitleComponent } from '../page-title/page-title.component';
import { PageHeaderComponent } from '../page-header/page-header.component';

import { HomeModule } from '../home/home.module';
import { AuthenticationService } from '../services/authentication.service';
import { AuthenticationGuard } from '../guards/authentication.guard';
import { ExercisesModule } from '../exercises/exercises.module';
import { RoutinesModule } from '../routines/routines.module';
import { CyclesModule } from '../cycles/cycles.module';
import { LogsModule } from '../logs/logs.module';
import { LoadingModule } from '../loading/loading.module';
import { DirectivesModule } from '../directives/directives.module';

export function initializeGoogleApi(authenticationService: AuthenticationService)
{
    return () => authenticationService.initializeClient();
}

@NgModule({
    declarations: [
        AppComponent,
        SideMenuComponent,
        PageTitleComponent,
        PageHeaderComponent,
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        AppRouterModule,
        HomeModule,
        ExercisesModule,
        RoutinesModule,
        LogsModule,
        CyclesModule,
        LoadingModule,
        DirectivesModule,
        SortablejsModule.forRoot({ animation: 150 })
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: initializeGoogleApi, deps: [AuthenticationService], multi: true },
        AuthenticationGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
