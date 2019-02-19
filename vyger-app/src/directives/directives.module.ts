import { NgModule } from '@angular/core';
import { AddListGroupItemDirective } from './add-list-group-item.directive';
import { DangerButtonDirective } from './danger-button.directive';
import { PrimaryButtonDirective } from './primary-button.directive';
import { SecondaryButtonDirective } from './secondary-button.directive';


@NgModule({
    declarations: [
        AddListGroupItemDirective,
        PrimaryButtonDirective,
        SecondaryButtonDirective,
        DangerButtonDirective
    ],
    exports: [
        AddListGroupItemDirective,
        PrimaryButtonDirective,
        SecondaryButtonDirective,
        DangerButtonDirective
    ]
})
export class DirectivesModule { }
