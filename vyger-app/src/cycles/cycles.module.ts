import { NgModule, Directive } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SortablejsModule } from 'angular-sortablejs';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LoadingModule } from 'src/loading/loading.module';
import { DirectivesModule } from 'src/directives/directives.module';
import { CycleListComponent } from './components/cycle-list/cycle-list.component';
import { CycleAddComponent } from './components/cycle-add/cycle-add.component';
import { CycleInputListComponent } from './components/cycle-input-list/cycle-input-list.component';
import { CycleExerciseListComponent } from './components/cycle-exercise-list/cycle-exercise-list.component';
import { CycleExerciseDaysPickerComponent } from './components/cycle-exercise-days-picker/cycle-exercise-days-picker.component';
import { CycleExerciseWeeksPickerComponent } from './components/cycle-exercise-weeks-picker/cycle-exercise-weeks-picker.component';

const routes: Routes = [
    {
        path: 'cycles',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: CycleListComponent },
            { path: 'add', component: CycleAddComponent },
            { path: 'inputs/:id', component: CycleInputListComponent },
            { path: 'exercises/:id', component: CycleExerciseListComponent },
        ]
    },
];

@NgModule({
    declarations: [
        CycleListComponent,
        CycleAddComponent,
        CycleInputListComponent,
        CycleExerciseListComponent,
        CycleExerciseWeeksPickerComponent,
        CycleExerciseDaysPickerComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        LoadingModule,
        DirectivesModule,
        SortablejsModule,
        RouterModule.forRoot(routes, { useHash: true })
    ]
})
export class CyclesModule { }
