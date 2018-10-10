import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PageTitleService } from 'src/page-title/page-title.service';
import { Cycle } from 'src/models/cycle';
import { RoutinesRepository } from 'src/routines/routines.repository';
import { CyclesRepository } from '../../cycles.repository';
import { Routine } from 'src/models/routine';
import { CycleInput } from 'src/models/cycle-input';
import { utilities } from 'src/models/utilities';

@Component({
    selector: 'app-cycle-add',
    templateUrl: './cycle-add.component.html',
    styleUrls: ['./cycle-add.component.css']
})
export class CycleAddComponent implements OnInit
{
    routines: Routine[];
    routine: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private cyclesRepository: CyclesRepository,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Add Cycle');

        this.routinesRepository
            .getRoutines()
            .then(this.onloadingRoutines);
    }

    private onloadingRoutines = (data: Routine[]) =>
    {
        this.routines = data.sort(Routine.compare);
    }

    cancel(): void
    {
        this.router.navigateByUrl('/cycles');
    }

    save(): void
    {
        this.saving = true;

        const id = utilities.generateId('c', 3);

        let cycle = new Cycle({ ...this.routine, id });

        cycle.inputs = this.getUniqueInputs(cycle);

        this.cyclesRepository
            .add(cycle)
            .then(() =>
            {
                this.saving = false;
                this.router.navigateByUrl('/cycles/inputs/' + cycle.id);
            });
    }

    private getUniqueInputs = (cycle: Cycle): CycleInput[] =>
    {
        let picked = [];

        let inputs = [];

        for (let i = 0; i < cycle.exercises.length; i++)
        {
            let e = cycle.exercises[i];

            if (e.containsRepMax && picked.indexOf(e.id) == -1)
            {
                inputs.push(new CycleInput({ ...e }));

                picked.push(e.id);
            }
        }

        return inputs;
    }
}
