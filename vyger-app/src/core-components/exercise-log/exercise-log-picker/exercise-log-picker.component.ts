import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import { Categories, Exercise, Groups } from 'src/models/exercise';
import { ExerciseLog } from 'src/models/exercise-log';
import { ExerciseService } from 'src/services/exercise.service';

@Component({
    selector: 'app-exercise-log-picker',
    templateUrl: './exercise-log-picker.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class ExerciseLogPickerComponent implements OnInit, OnChanges
{
    groups: string[];
    categories: string[];
    exercises: Exercise[];

    @Input() logs: ExerciseLog[];
    @Input() exercise: ExerciseLog;

    constructor(
        private ExerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.loadGroups();
        this.loadCategories();
    }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.logs.currentValue && change.logs.previousValue == null)
        {
            this.loadExercises();
        }
    }

    loadGroups(): void
    {
        this.groups = [];

        for (let x in Groups)
        {
            this.groups.push(x);
        }
    }

    loadCategories(): void
    {
        this.categories = [];

        for (let x in Categories)
        {
            this.categories.push(x);
        }
    }

    loadExercises(): void
    {
        this.ExerciseService
            .getExercises()
            .then(this.onloadingExercises);
    }

    private onloadingExercises = (exercises: Exercise[]) =>
    {
        const picked = this.getPickedList();

        this.exercises = exercises
            .filter(x => !picked.includes(x.id))
            .sort(Exercise.compare);
    }

    private getPickedList = (): string[] =>
    {
        let picked: string[] = [];

        for (let i = 0; i < this.logs.length; i++)
        {
            let id = this.logs[i].id;

            if (!picked.includes(id))
            {
                picked.push(id);
            }
        }

        return picked;
    }

    getExercises(): Exercise[]
    {
        if (this.exercises)
        {
            return this.exercises
                .filter(x => this.exercise.group == null || this.exercise.group == x.group)
                .filter(x => this.exercise.category == null || this.exercise.category == x.category);
        }

        return [];
    }
}
