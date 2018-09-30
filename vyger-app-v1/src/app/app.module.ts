import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app-component';
import { AppRoutingModule } from './app-routine.module';
import { ServicesModule } from 'src/services/services.module';
import { GuardsModule } from 'src/guards/guards.module';
import { ComponentsModule } from './components/components.module';
import { PagesModule } from './pages/pages.module';

import { AuthenticationService } from 'src/services/authentication.service';

export function initGapi(authenticationService: AuthenticationService) {
    return () => authenticationService.initClient();
}

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        HttpClientModule,
        AppRoutingModule,
        ServicesModule,
        GuardsModule,
        ComponentsModule,
        PagesModule
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: initGapi, deps: [AuthenticationService], multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
