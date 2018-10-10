import { Component, OnInit, Input } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Routine } from 'src/models/routine';

@Component({
    selector: 'app-routine-name',
    templateUrl: './routine-name.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class RoutineNameComponent implements OnInit
{
    @Input() routine: Routine;

    constructor() { }

    ngOnInit()
    {
    }
}
