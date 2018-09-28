
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationGuard } from './authentication.guard';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    AuthenticationGuard
  ],
  exports: [
    AuthenticationGuard
  ]
})
export class GuardsModule { }
