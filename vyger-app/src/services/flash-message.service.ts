import { Injectable } from '@angular/core';
import { FlashMessage } from 'src/models/flash-message';

@Injectable({
    providedIn: 'root'
})
export class FlashMessageService
{
    messages: FlashMessage[];

    constructor()
    {
        this.messages = [];
    }

    remove(msg: FlashMessage): void
    {
        let idx = this.messages.indexOf(msg);

        if (idx > -1)
        {
            this.messages.splice(idx, 1);
        }
    }
}
