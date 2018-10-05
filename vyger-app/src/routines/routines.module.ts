import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationGuard } from 'src/guards/authentication.guard';

import { RoutineListComponent } from './components/routine-list/routine-list.component';
import { RoutineAddComponent } from './components/routine-add/routine-add.component';
import { RoutineEditComponent } from './components/routine-edit/routine-edit.component';
import { RoutineNameComponent } from './components/routine-name/routine-name.component';
import { RoutineNameValidatorDirective } from './directives/routine-name-validator.directive';
import { LoadingModule } from '../loading/loading.module';
import { RoutineWeeksComponent } from './components/routine-weeks/routine-weeks.component';
import { RoutineDaysComponent } from './components/routine-days/routine-days.component';

const routes: Routes = [
    {
        path: 'routines',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: RoutineListComponent },
            { path: 'add', component: RoutineAddComponent },
            { path: 'edit/:id', component: RoutineEditComponent },
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
        RoutineDaysComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        LoadingModule,
        RouterModule.forRoot(routes, { useHash: true })
    ]
})
export class RoutinesModule { }
