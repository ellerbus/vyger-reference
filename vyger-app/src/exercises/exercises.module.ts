import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LoadingModule } from 'src/loading/loading.module';
import { DirectivesModule } from 'src/directives/directives.module';

import { ExerciseListComponent } from './components/exercise-list/exercise-list.component';
import { ExerciseAddComponent } from './components/exercise-add/exercise-add.component';
import { ExerciseEditComponent } from './components/exercise-edit/exercise-edit.component';
import { ExerciseGroupComponent } from './components/exercise-group/exercise-group.component';
import { ExerciseCategoryComponent } from './components/exercise-category/exercise-category.component';
import { ExerciseNameComponent } from './components/exercise-name/exercise-name.component';
import { ExerciseNameValidatorDirective } from './directives/exercise-name-validator.directive';

const routes: Routes = [
    {
        path: 'exercises',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: ExerciseListComponent },
            { path: 'add', component: ExerciseAddComponent },
            { path: 'edit/:id', component: ExerciseEditComponent },
        ]
    },
];

@NgModule({
    declarations: [
        ExerciseListComponent,
        ExerciseAddComponent,
        ExerciseGroupComponent,
        ExerciseCategoryComponent,
        ExerciseNameComponent,
        ExerciseNameValidatorDirective,
        ExerciseEditComponent,
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        LoadingModule,
        DirectivesModule,
        RouterModule.forRoot(routes, { useHash: true })
    ]
})
export class ExercisesModule { }
