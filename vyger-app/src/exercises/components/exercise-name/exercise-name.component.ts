import { Component, OnInit, Input } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Exercise } from '../../models/exercise';

@Component({
    selector: 'app-exercise-name',
    templateUrl: './exercise-name.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class ExerciseNameComponent implements OnInit {
    @Input() exercise: Exercise;

    constructor() { }

    ngOnInit() {
    }
}
