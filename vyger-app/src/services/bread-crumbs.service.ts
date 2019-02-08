import { Injectable } from '@angular/core';
import { BreadCrumb } from 'src/common-components/bread-crumbs/bread-crumb.model';

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

    add(title: string, path: string = null): void
    {
        let bc = new BreadCrumb(title, path, path == null);

        this.crumbs.push(bc);
    }

    clear(): void
    {
        this.crumbs = [];
    }
}
