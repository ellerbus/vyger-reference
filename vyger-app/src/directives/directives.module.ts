import { NgModule } from '@angular/core';
import { AddListGroupItemDirective } from './add-list-group-item.directive';
import { PrimaryButtonDirective } from './primary-button.directive';
import { SecondaryButtonDirective } from './secondary-button.directive';


@NgModule({
    declarations: [
        AddListGroupItemDirective,
        PrimaryButtonDirective,
        SecondaryButtonDirective,
    ],
    exports: [
        AddListGroupItemDirective,
        PrimaryButtonDirective,
        SecondaryButtonDirective,
    ]
})
export class DirectivesModule { }
