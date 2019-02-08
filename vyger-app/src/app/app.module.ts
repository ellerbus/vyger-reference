import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { SortablejsModule } from 'angular-sortablejs';
import { AppComponent } from 'src/app/app.component';
import { BreadCrumbsComponent } from 'src/common-components/bread-crumbs/bread-crumbs.component';
import { CyclesModule } from 'src/cycles/cycles.module';
import { DirectivesModule } from 'src/directives/directives.module';
import { ExercisesModule } from 'src/exercises/exercises.module';
import { FlashMessagesComponent } from 'src/common-components/flash-messages/flash-messages.component';
import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LoadingComponent } from 'src/common-components/loading/loading.component';
import { LogsModule } from 'src/logs/logs.module';
import { PageHeaderComponent } from 'src/common-components/page-header/page-header.component';
import { PageTitleComponent } from 'src/common-components/page-title/page-title.component';
import { RoutinesModule } from 'src/routines/routines.module';
import { AuthenticationService } from 'src/services/authentication.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { HttpErrorInterceptorService } from 'src/services/http-error-interceptor.service';
import { SideMenuComponent } from 'src/side-menu/side-menu.component';
import { HomeComponent } from 'src/pages/home/home.component';
import { SignInComponent } from 'src/pages/sign-in/sign-in.component';

export function initializeGoogleApi(authenticationService: AuthenticationService)
{
    return () => authenticationService.initializeClient();
}

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
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
        AppComponent,
        SideMenuComponent,
        PageTitleComponent,
        PageHeaderComponent,
        FlashMessagesComponent,
        BreadCrumbsComponent,
        HomeComponent,
        SignInComponent
        //LoadingComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        ExercisesModule,
        RoutinesModule,
        LogsModule,
        CyclesModule,
        DirectivesModule,
        SortablejsModule.forRoot({ animation: 150 }),
        RouterModule.forRoot(routes, { useHash: true })
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: initializeGoogleApi, multi: true, deps: [AuthenticationService] },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptorService, multi: true, deps: [FlashMessageService] },
        AuthenticationGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
