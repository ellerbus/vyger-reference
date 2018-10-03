import { Component, OnInit, Input } from '@angular/core';

import { Exercise, Groups } from '../../models/exercise';

@Component({
    selector: 'app-exercise-group',
    templateUrl: './exercise-group.component.html'
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
