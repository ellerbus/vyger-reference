import { Directive, HostBinding } from '@angular/core';

@Directive({
    selector: '[dangerButton]'
})
export class DangerButtonDirective
{
    @HostBinding('class') classes = 'btn btn-danger ml-2';
}
