import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Exercise } from 'src/models/exercise';
import { BreadCrumbsService } from 'src/services/bread-crumbs.service';
import { ExerciseService } from 'src/services/exercise.service';
import { PageTitleService } from 'src/services/page-title.service';

@Component({
    selector: 'app-exercise-list',
    templateUrl: './exercise-list.component.html'
})
export class ExerciseListComponent implements OnInit
{
    group: string;
    category: string;

    all: Exercise[];
    exercises: Exercise[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private breadCrumbService: BreadCrumbsService,
        private pageTitleService: PageTitleService,
        private exerciseService: ExerciseService) { }

    ngOnInit()
    {
        this.pageTitleService.setTitle('Exercises');

        this.exerciseService
            .getExercises()
            .then(this.onloadingExercises);

        this.activatedRoute.queryParams.subscribe((x: Params) =>
        {
            this.group = x.group ? x.group : null;
            this.category = x.category ? x.category : null;

            this.filterExercises();

            this.updateBreadCrumbs();
        });
    }

    private onloadingExercises = (data: Exercise[]) =>
    {
        this.all = data.sort(Exercise.compare);

        this.filterExercises();
    };

    private filterExercises = () =>
    {
        if (this.all)
        {
            this.exercises = this.all.filter(this.filterExercise);
        }
    };

    private filterExercise = (ex: Exercise): boolean =>
    {
        if (this.group)
        {
            return ex.group == this.group;
        }

        if (this.category)
        {
            return ex.category == this.category;
        }

        return true;
    };

    private updateBreadCrumbs = () =>
    {
        this.breadCrumbService.add('Home', '/');

        if (this.group)
        {
            this.breadCrumbService.add('Exercises', null, 'Group=' + this.group);
        }
        else if (this.category)
        {
            this.breadCrumbService.add('Exercises', null, 'Category=' + this.category);
        }
        else
        {
            this.breadCrumbService.add('Exercises');
        }
    };
}