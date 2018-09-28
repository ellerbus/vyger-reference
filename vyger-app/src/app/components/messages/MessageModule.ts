import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageListComponent } from './components/MessageListComponent';

@NgModule({
  declarations: [
    MessageListComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    MessageListComponent
  ]
})
export class MessageModule { }
