import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { DataGuard } from 'src/guards/data.guard';

import { ExercisesComponent } from './components/exercises/exercises.component';
import { ExerciseAddComponent } from './components/exercise-add/exercise-add.component';

const routes: Routes = [
    {
        path: 'exercises', canActivate: [AuthenticationGuard, DataGuard],
        children: [
            { path: '', component: ExercisesComponent },
        ]
    },
];

@NgModule({
    declarations: [ExercisesComponent, ExerciseAddComponent],
    imports: [
        BrowserModule,
        RouterModule,
        RouterModule.forRoot(routes, { useHash: true })
    ]
})
export class ExercisesModule { }
