import { Component, OnInit, Input } from '@angular/core';

import { Exercise, Categories } from '../../models/exercise';

@Component({
    selector: 'app-exercise-category',
    templateUrl: './exercise-category.component.html'
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
