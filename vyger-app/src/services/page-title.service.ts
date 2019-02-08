import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
    providedIn: 'root'
})
export class PageTitleService
{
    private title: string = 'Vyger';
    private subtitle: string = null;

    constructor(
        private titleService: Title)
    {
        this.titleService.setTitle(this.title);
    }

    getTitle(): string
    {
        return this.title;
    }

    setTitle(title: string, subtitle: string = null): void
    {
        this.title = title;

        let display = this.title;
        if (this.title != 'Vyger')
        {
            display = 'Vyger - ' + display;
        }

        this.titleService.setTitle(display);

        this.subtitle = subtitle;
    }

    getSubTitle(): string
    {
        return this.subtitle;
    }

    setSubTitle(subtitle: string): void
    {
        this.subtitle = 'm';
    }
}
