import { Component, OnInit } from '@angular/core';
import { FlashMessageService } from 'src/services/flash-message.service';

@Component({
    selector: 'app-flash-messages',
    templateUrl: './flash-messages.component.html',
    styleUrls: ['./flash-messages.component.css']
})
export class FlashMessagesComponent implements OnInit
{

    constructor(public flashMessageService: FlashMessageService) { }

    ngOnInit()
    {
    }

}
