import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Routine } from '../../../models/routine';
import { RoutinesRepository } from '../../routines.repository';
import { PageTitleService } from '../../../page-title/page-title.service';
import { utilities } from '../../../models/utilities';

@Component({
    selector: 'app-routine-edit',
    templateUrl: './routine-edit.component.html',
    styleUrls: ['./routine-edit.component.css']
})
export class RoutineEditComponent implements OnInit
{
    routine: Routine;
    clone: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Edit Routine');

        this.routinesRepository
            .getRoutines()
            .then(this.onloadingRoutine);
    }

    private onloadingRoutine = (routines: Routine[]): void =>
    {
        const id = this.activatedRoute.snapshot.paramMap.get('id');

        for (let i = 0; i < routines.length; i++)
        {
            if (routines[i].id == id)
            {
                this.routine = routines[i];
                break;
            }
        }

        if (this.routine == null)
        {
            this.router.navigateByUrl('/routines');
        } else
        {
            this.clone = { ...this.routine };
        }
    }

    cancel(): void
    {
        this.router.navigateByUrl('/routines');
    }

    save(): void
    {
        const keys = ['name', 'week', 'days'];

        utilities.extend(this.routine, this.clone, keys);

        this.saving = true;

        this.routinesRepository
            .save()
            .then(() =>
            {
                this.router.navigateByUrl('/routines');
            })
            .then(() =>
            {
                this.saving = false;
            });
    }
}
