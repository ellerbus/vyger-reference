import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadCrumb } from 'src/models/bread-crumb.model';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';

@Component({
    selector: 'app-bread-crumbs',
    templateUrl: './bread-crumbs.component.html',
    styleUrls: ['./bread-crumbs.component.css']
})
export class BreadCrumbsComponent implements OnInit
{
    constructor(
        private activatedRoute: ActivatedRoute,
        private breadCrumbsService: BreadCrumbsService)
    {
    }

    ngOnInit()
    {
    }

    hasCrumbs(): boolean
    {
        return this.breadCrumbsService.crumbs && this.breadCrumbsService.crumbs.length > 0;
    }

    getCrumbs(): BreadCrumb[]
    {
        if (this.hasCrumbs())
        {
            return [...this.breadCrumbsService.crumbs];
        }

        return [];
    }
}
