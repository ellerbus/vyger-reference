import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';

import { Groups, Exercise, Categories } from 'src/models/exercise';
import { LogExercise } from 'src/models/log-exercise';
import { ExercisesRepository } from 'src/exercises/exercises.repository';
import { LogsRepository } from 'src/logs/logs.repository';

@Component({
    selector: 'app-log-exercise-picker',
    templateUrl: './log-exercise-picker.component.html',
    styleUrls: ['./log-exercise-picker.component.css'],
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class LogExercisePickerComponent implements OnInit, OnChanges
{
    groups: string[];
    categories: string[];
    exercises: Exercise[];

    @Input() exercise: LogExercise;
    @Output() exerciseChange = new EventEmitter<LogExercise>();

    constructor(
        private logRepository: LogsRepository,
        private exercisesRepository: ExercisesRepository) { }

    ngOnInit()
    {
        this.loadGroups();
        this.loadCategories();
    }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.exercise.currentValue && change.exercise.previousValue == null)
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
        this.exercisesRepository
            .getExercises()
            .then(this.onloadingExercises);
    }

    private onloadingExercises = (exercises: Exercise[]) =>
    {
        this.getPickedList(exercises);
    }

    private getPickedList = (exercises: Exercise[]) =>
    {
        this.logRepository
            .getLogsFor(this.exercise.ymd)
            .then((logged: LogExercise[]) =>
            {
                let picked = logged.map(x => x.id);

                this.exercises = exercises
                    .filter(x => !picked.includes(x.id))
                    .sort(Exercise.compare);
            });
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

    update()
    {
        this.exerciseChange.emit(this.exercise);
    }

    // loadMostRecent()
    // {
    //     if (this.exercise.sets.length == 0 && this.exercise.id)
    //     {
    //         this.logRepository
    //             .getMostRecent(this.exercise.id)
    //             .then(this.onloadingMostRecent);
    //     }
    // }

    // private onloadingMostRecent = (mostrecent: LogExercise) =>
    // {
    //     if (mostrecent)
    //     {
    //         this.exercise.sets = mostrecent.sets;
    //     }
    // }
}
