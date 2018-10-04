import { Component, OnInit, Input } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Exercise, Groups } from '../../models/exercise';

@Component({
    selector: 'app-exercise-group',
    templateUrl: './exercise-group.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class ExerciseGroupComponent implements OnInit {
    groups: string[];
    @Input() exercise: Exercise;

    constructor() { }

    ngOnInit() {
        this.loadGroups();
    }

    loadGroups(): void {
        this.groups = [];
        for (let x in Groups) {
            this.groups.push(x);
        }
    }
}
