import { Directive, HostBinding } from '@angular/core';

@Directive({
    selector: '[secondaryButton]'
})
export class SecondaryButtonDirective
{
    @HostBinding('class') classes = 'btn btn-outline-secondary';
}
