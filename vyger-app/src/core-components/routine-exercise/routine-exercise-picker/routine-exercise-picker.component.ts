import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import { Categories, Exercise, Groups } from 'src/models/exercise';
import { Routine } from 'src/models/routine';
import { RoutineExercise } from 'src/models/routine-exercise';
import { ExerciseService } from 'src/services/exercise.service';

@Component({
    selector: 'app-routine-exercise-picker',
    templateUrl: './routine-exercise-picker.component.html',
    viewProviders: [
        { provide: ControlContainer, useExisting: NgForm }
    ]
})
export class RoutineExercisePickerComponent implements OnInit, OnChanges
{
    groups: string[];
    categories: string[];
    exercises: Exercise[];

    @Input() routine: Routine;
    @Input() exercise: RoutineExercise;
    @Input() day: number;

    constructor(
        private ExerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.loadGroups();
        this.loadCategories();
    }

    ngOnChanges(change: SimpleChanges)
    {
        if (change.routine.currentValue && change.routine.previousValue == null)
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

        for (let i = 0; i < this.routine.exercises.length; i++)
        {
            let ex = this.routine.exercises[i];

            if (ex.day == this.day)
            {
                if (!picked.includes(ex.id))
                {
                    picked.push(ex.id);
                }
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
