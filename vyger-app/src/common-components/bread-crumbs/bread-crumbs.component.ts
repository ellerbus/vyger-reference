import { Component, OnInit } from '@angular/core';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';

@Component({
    selector: 'app-bread-crumbs',
    templateUrl: './bread-crumbs.component.html',
    styleUrls: ['./bread-crumbs.component.css']
})
export class BreadCrumbsComponent implements OnInit
{
    constructor(
        public breadCrumbsService: BreadCrumbsService)
    {
    }

    ngOnInit()
    {
    }
}
