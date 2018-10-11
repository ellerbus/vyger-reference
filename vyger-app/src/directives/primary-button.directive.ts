import { Directive, HostBinding } from '@angular/core';

@Directive({
    selector: '[primaryButton]'
})
export class PrimaryButtonDirective
{
    @HostBinding('class') classes = 'btn btn-primary ml-2';
}
