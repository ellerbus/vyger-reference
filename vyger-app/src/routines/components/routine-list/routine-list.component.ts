import { Component, OnInit } from '@angular/core';
import { PageTitleService } from 'src/page-title/page-title.service';
import { Routine } from '../../../models/routine';

import { RoutinesRepository } from '../../routines.repository';

@Component({
    selector: 'app-routine-list',
    templateUrl: './routine-list.component.html',
    styleUrls: ['./routine-list.component.css']
})
export class RoutineListComponent implements OnInit {
    routines: Routine[];

    constructor(
        private pageTitleService: PageTitleService,
        private routinesRepository: RoutinesRepository) { }

    ngOnInit() {
        this.pageTitleService.setTitle('Routines');

        this.routinesRepository
            .getRoutines()
            .then(this.onloadingRoutines);
    }

    private onloadingRoutines = (data: Routine[]) => {
        this.routines = data.sort(Routine.compare);
    };
}
