import { Directive, HostBinding } from '@angular/core';

@Directive({
    selector: '[addListGroupItem]'
})
export class AddListGroupItemDirective
{
    @HostBinding('class') classes = 'list-group-item list-group-item-action text-right text-success';
    @HostBinding('style.text-transform') upper = 'uppercase';
}
