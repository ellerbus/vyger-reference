import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
    providedIn: 'root'
})
export class PageTitleService
{
    private title: string = 'vyger';

    constructor(
        private titleService: Title)
    {
        this.titleService.setTitle(this.title);
    }

    getTitle(): string
    {
        return this.title;
    }

    setTitle(title: string): void
    {
        this.title = title;

        let display = this.title;

        if (this.title != 'vyger')
        {
            display = 'vyger - ' + display;
        }

        this.titleService.setTitle(display);
    }
}
