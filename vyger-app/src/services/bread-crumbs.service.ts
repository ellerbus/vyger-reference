import { Injectable } from '@angular/core';
import { BreadCrumb } from 'src/models/bread-crumb.model';

@Injectable({
    providedIn: 'root'
})
export class BreadCrumbsService
{
    crumbs: BreadCrumb[];

    constructor()
    {
        this.crumbs = [];
    }

    add(title: string, path: string = null, filter: string = null): void
    {
        let bc = new BreadCrumb(title, path, path == null, filter);

        this.crumbs.push(bc);
    }

    clear(): void
    {
        this.crumbs = [];
    }
}
