import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-heading',
    template: '<h3 [innerText]="heading"></h3>'
})
export class HeadingComponent {

    @Input() heading: string;

    constructor() { }
}
