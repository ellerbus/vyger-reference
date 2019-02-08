import { Injectable } from '@angular/core';
import { FlashMessage, FlashMessageSeverity } from 'src/models/flash-message';

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

    clear()
    {
        this.messages = this.messages.filter(x => x.growl);
    }

    remove(msg: FlashMessage): void
    {
        let idx = this.messages.indexOf(msg);

        if (idx > -1)
        {
            this.messages.splice(idx, 1);
        }
    }

    info(message: string, growl: boolean = false)
    {
        this.add(FlashMessageSeverity.Information, message, growl);
    }

    success(message: string, growl: boolean = false)
    {
        this.add(FlashMessageSeverity.Success, message, growl);
    }

    danger(message: string, growl: boolean = false)
    {
        this.add(FlashMessageSeverity.Danger, message, growl);
    }

    private add(severity: FlashMessageSeverity, message: string, growl: boolean)
    {
        let options = {
            severity,
            message,
            growl
        };

        let msg = new FlashMessage(options);

        this.messages.push(msg);
    }
}
