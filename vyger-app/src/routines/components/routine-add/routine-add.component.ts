import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Routine } from 'src/models/routine';
import { PageTitleService } from 'src/services/page-title.service';
import { RoutinesRepository } from '../../routines.repository';
import { RoutineExercise } from 'src/models/routine-exercise';

@Component({
    selector: 'app-routine-add',
    templateUrl: './routine-add.component.html',
    styleUrls: ['./routine-add.component.css']
})
export class RoutineAddComponent implements OnInit
{
    categories: string[];
    routine: Routine;
    exercise: RoutineExercise;
    saving: boolean;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository)
    {
        this.routine = new Routine();
        this.exercise = new RoutineExercise();

        this.exercise.sets = ['5RM-90%x5'];
    }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Add Routine');
    }

    cancel(): void
    {
        this.router.navigateByUrl('/routines/exercises/' + this.routine.id);
    }

    save(): void
    {
        this.saving = true;

        this.routine.sets = this.exercise.sets;

        this.routinesRepository
            .add(this.routine)
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/routines/exercises/' + this.routine.id);
            });
    }
}
