import { NgModule, Directive } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LoadingModule } from 'src/loading/loading.module';
import { DirectivesModule } from 'src/directives/directives.module';

import { RoutineListComponent } from './components/routine-list/routine-list.component';
import { RoutineAddComponent } from './components/routine-add/routine-add.component';
import { RoutineEditComponent } from './components/routine-edit/routine-edit.component';
import { RoutineNameComponent } from './components/routine-name/routine-name.component';
import { RoutineNameValidatorDirective } from './directives/routine-name-validator.directive';
import { RoutineWeeksComponent } from './components/routine-weeks/routine-weeks.component';
import { RoutineDaysComponent } from './components/routine-days/routine-days.component';
import { RoutineExerciseListComponent } from './components/routine-exercise-list/routine-exercise-list.component';
import { RoutineExerciseAddComponent } from './components/routine-exercise-add/routine-exercise-add.component';
import { RoutineExerciseDayOfWeekComponent } from './components/routine-exercise-day-of-week/routine-exercise-day-of-week.component';
import { RoutineExercisePickerComponent } from './components/routine-exercise-picker/routine-exercise-picker.component';
import { RoutineExerciseWeeksPickerComponent } from './components/routine-exercise-weeks-picker/routine-exercise-weeks-picker.component';
import { RoutineExerciseDaysPickerComponent } from './components/routine-exercise-days-picker/routine-exercise-days-picker.component';
import { RoutineExerciseSetComponent } from './components/routine-exercise-set/routine-exercise-set.component';
import { RoutineExerciseSetsComponent } from './components/routine-exercise-sets/routine-exercise-sets.component';

const routes: Routes = [
    {
        path: 'routines',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: RoutineListComponent },
            { path: 'add', component: RoutineAddComponent },
            { path: 'edit/:id', component: RoutineEditComponent },
            { path: 'exercises/:id', component: RoutineExerciseListComponent },
            { path: 'exercises/:id/add', component: RoutineExerciseAddComponent },
        ]
    },
];

@NgModule({
    declarations: [
        RoutineListComponent,
        RoutineAddComponent,
        RoutineNameComponent,
        RoutineNameValidatorDirective,
        RoutineEditComponent,
        RoutineWeeksComponent,
        RoutineDaysComponent,
        RoutineExerciseListComponent,
        RoutineExerciseAddComponent,
        RoutineExerciseDayOfWeekComponent,
        RoutineExercisePickerComponent,
        RoutineExerciseWeeksPickerComponent,
        RoutineExerciseDaysPickerComponent,
        RoutineExerciseSetComponent,
        RoutineExerciseSetsComponent,
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
export class RoutinesModule { }
