import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Routine } from '../../../models/routine';
import { PageTitleService } from 'src/page-title/page-title.service';
import { RoutinesRepository } from '../../routines.repository';

@Component({
    selector: 'app-routine-add',
    templateUrl: './routine-add.component.html',
    styleUrls: ['./routine-add.component.css']
})
export class RoutineAddComponent implements OnInit {
    categories: string[];
    routine: Routine;
    saving: boolean;

    constructor(
        private router: Router,
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Add Routine');
        this.loadRoutine();
    }

    loadRoutine(): void {
        this.routine = new Routine();
    }

    cancel(): void {
        this.router.navigateByUrl('/routines');
    }

    save(): void {
        this.saving = true;

        this.routinesRepository
            .add(this.routine)
            .then(() => {
                this.router.navigateByUrl('/routines');
            })
            .then(() => {
                this.saving = false;
            });
    }
}
