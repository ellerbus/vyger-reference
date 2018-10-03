import { Component, OnInit, Input } from '@angular/core';

import { Exercise } from '../../models/exercise';

@Component({
    selector: 'app-exercise-name',
    templateUrl: './exercise-name.component.html'
})
export class ExerciseNameComponent implements OnInit {
    @Input() exercise: Exercise;

    constructor() { }

    ngOnInit() {
    }
}
