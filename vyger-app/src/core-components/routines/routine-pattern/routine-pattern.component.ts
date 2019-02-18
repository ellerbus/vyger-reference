import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import { Routine } from 'src/models/routine';

@Component({
    selector: 'app-routine-pattern',
    templateUrl: './routine-pattern.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class RoutinePatternComponent implements OnInit
{
    @Input() routine: Routine;

    constructor() { }

    ngOnInit()
    {
    }
}
