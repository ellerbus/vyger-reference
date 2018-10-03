import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { DataGuard } from 'src/guards/data.guard';

import { ExercisesComponent } from './components/exercises/exercises.component';
import { ExerciseAddComponent } from './components/exercise-add/exercise-add.component';
import { ExerciseGroupComponent } from './components/exercise-group/exercise-group.component';
import { ExerciseCategoryComponent } from './components/exercise-category/exercise-category.component';
import { ExerciseNameComponent } from './components/exercise-name/exercise-name.component';

const routes: Routes = [
    {
        path: 'exercises', canActivate: [AuthenticationGuard, DataGuard],
        children: [
            { path: '', component: ExercisesComponent },
            { path: 'add', component: ExerciseAddComponent },
        ]
    },
];

@NgModule({
    declarations: [
        ExercisesComponent,
        ExerciseAddComponent,
        ExerciseGroupComponent,
        ExerciseCategoryComponent,
        ExerciseNameComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        RouterModule.forRoot(routes, { useHash: true })
    ]
})
export class ExercisesModule { }
