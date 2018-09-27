import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './components/DashboardComponent';
import { MessageModule } from '../messages/MessageModule';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MessageModule
  ],
  exports: [
    DashboardComponent
  ]
})
export class DashboardModule { }
