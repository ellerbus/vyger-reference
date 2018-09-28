import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomePageComponent } from './home/home.page';
import { DataPageComponent } from './data/data.page';
import { SignInPageComponent } from './signin/signin.page';

@NgModule({
  declarations: [
    HomePageComponent,
    DataPageComponent,
    SignInPageComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HomePageComponent,
    DataPageComponent,
    SignInPageComponent
  ]
})
export class PagesModule { }
