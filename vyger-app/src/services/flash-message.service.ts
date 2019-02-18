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

        setInterval(this.callback, 1000);
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

    private callback = () =>
    {
        for (let i = 0; i < this.messages.length; i++)
        {
            let msg = this.messages[i];

            msg.seconds += 1;

            if (msg.growl && msg.seconds >= 15)
            {
                this.messages.splice(i, 1);

                i -= 1;
            }
        }
    };
}
