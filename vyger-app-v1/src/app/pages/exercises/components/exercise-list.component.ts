import { Component, OnInit } from '@angular/core';
import { ExerciseRepository } from '../../../repositories/exercise.respository';
import { Exercise } from '../../../models/exercise';

@Component({
    selector: 'app-exercise-list',
    templateUrl: './exercise-list.component.html'
})
export class ExerciseListComponent implements OnInit {
    public exercises: Exercise[];
    filter: Exercise;

    constructor(
        private exerciseRepository: ExerciseRepository) { }

    ngOnInit() {
        this.filter = new Exercise();

        this.exerciseRepository
            .getExercises()
            .subscribe(this.onloadingExercises);
    }

    private onloadingExercises = (data: Exercise[]) => {
        this.exercises = data.sort(this.compareExercise);
    };

    private compareExercise = (a: Exercise, b: Exercise): number => {
        var group = a.group.localeCompare(b.group);

        if (group != 0) {
            return group;
        }

        var category = a.category.localeCompare(b.category);

        if (category != 0) {
            return category;
        }

        return a.name.localeCompare(b.name);
    }

    getExercises(): Exercise[] {
        if (this.exercises) {
            return this.exercises
                .filter(this.filterByGroup)
                .filter(this.filterByCategory);
        }

        return [];
    }

    private filterByGroup = (data: Exercise) => {
        if (this.filter.group) {
            return this.filter.group == data.group;
        }

        return true;
    };

    private filterByCategory = (data: Exercise) => {
        if (this.filter.category) {
            return this.filter.category == data.category;
        }

        return true;
    };

    toggleGroupFilter(data: Exercise): void {
        if (this.filter.group) {
            this.filter.group = null;
        }
        else {
            this.filter.group = data.group;
        }
    }

    toggleCategoryFilter(data: Exercise): void {
        if (this.filter.category) {
            this.filter.category = null;
        }
        else {
            this.filter.category = data.category;
        }
    }
}
