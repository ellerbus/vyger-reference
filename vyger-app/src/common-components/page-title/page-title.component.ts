import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-page-title',
    templateUrl: './page-title.component.html',
    styleUrls: ['./page-title.component.css']
})
export class PageTitleComponent implements OnInit
{

    constructor(
        private pageTitleService: PageTitleService)
    {
    }

    ngOnInit()
    {
    }

    getTitle(): string
    {
        return this.pageTitleService.getTitle();
    }

    getSubTitle(): string
    {
        return this.pageTitleService.getSubTitle();
    }
}
