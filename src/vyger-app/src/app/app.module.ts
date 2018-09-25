import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HeroListComponent } from './heroes/components/HeroListComponent';
import { HeroDetailComponent } from './heroes/components/HeroDetailComponent';
import { MessagesComponent } from './messages/components/MessagesComponent';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './dashboard/components/DashboardComponent';

@NgModule({
  declarations: [
    AppComponent,
    HeroListComponent,
    HeroDetailComponent,
    MessagesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
