import { Component, OnInit } from '@angular/core';
import { MessageService } from '../services/MessageService';

@Component({
  selector: 'app-messages',
  templateUrl: './MessageListComponent.html',
  styleUrls: ['./MessageListComponent.css']
})
export class MessageListComponent implements OnInit {

  constructor(public messageService: MessageService) { }

  ngOnInit() {
  }

}
