import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { SortablejsModule } from 'angular-sortablejs';
import { FlashMessageService } from 'src/services/flash-message.service';
import { HttpErrorInterceptorService } from 'src/services/http-error-interceptor.service';
import { CyclesModule } from '../cycles/cycles.module';
import { DirectivesModule } from '../directives/directives.module';
import { ExercisesModule } from '../exercises/exercises.module';
import { FlashMessagesComponent } from '../flash-messages/flash-messages.component';
import { AuthenticationGuard } from '../guards/authentication.guard';
import { HomeModule } from '../home/home.module';
import { LoadingModule } from '../loading/loading.module';
import { LogsModule } from '../logs/logs.module';
import { PageHeaderComponent } from '../page-header/page-header.component';
import { PageTitleComponent } from '../page-title/page-title.component';
import { RoutinesModule } from '../routines/routines.module';
import { AuthenticationService } from '../services/authentication.service';
import { SideMenuComponent } from '../side-menu/side-menu.component';
import { AppRouterModule } from './app-router.module';
import { AppComponent } from './app.component';



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
        FlashMessagesComponent,
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
        { provide: APP_INITIALIZER, useFactory: initializeGoogleApi, multi: true, deps: [AuthenticationService] },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptorService, multi: true, deps: [FlashMessageService] },
        AuthenticationGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
