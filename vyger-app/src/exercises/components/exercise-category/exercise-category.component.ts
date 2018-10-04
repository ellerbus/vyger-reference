import { Component, OnInit, Input } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Exercise, Categories } from '../../models/exercise';

@Component({
    selector: 'app-exercise-category',
    templateUrl: './exercise-category.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class ExerciseCategoryComponent implements OnInit {
    categories: string[];
    @Input() exercise: Exercise;

    constructor() { }

    ngOnInit() {
        this.loadCategories();
    }

    loadCategories(): void {
        this.categories = [];
        for (let x in Categories) {
            this.categories.push(x);
        }
    }
}
