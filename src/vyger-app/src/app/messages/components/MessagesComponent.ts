import { Component, OnInit } from '@angular/core';
import { MessageService } from '../services/MessageService';

@Component({
  selector: 'app-messages',
  templateUrl: './MessagesComponent.html',
  styleUrls: ['./MessagesComponent.css']
})
export class MessagesComponent implements OnInit {

  constructor(public messageService: MessageService) { }

  ngOnInit() {
  }

}
