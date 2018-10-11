import { NgModule, Directive } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SortablejsModule } from 'angular-sortablejs';

import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LoadingModule } from 'src/loading/loading.module';
import { DirectivesModule } from 'src/directives/directives.module';
import { LogListComponent } from './components/log-list/log-list.component';
import { LogListHeaderComponent } from './components/log-list-header/log-list-header.component';
import { LogExerciseAddComponent } from './components/log-exercise-add/log-exercise-add.component';
import { LogExercisePickerComponent } from './components/log-exercise-picker/log-exercise-picker.component';
import { LogExerciseSetsComponent } from './components/log-exercise-sets/log-exercise-sets.component';
import { LogExerciseSetComponent } from './components/log-exercise-set/log-exercise-set.component';
import { LogExerciseEditComponent } from './components/log-exercise-edit/log-exercise-edit.component';
import { LogImportComponent } from './components/log-import/log-import.component';
import { LogHistoryComponent } from './components/log-history/log-history.component';

const routes: Routes = [
    {
        path: 'logs',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: LogListComponent },
            { path: 'add', component: LogExerciseAddComponent },
            { path: 'edit/:id', component: LogExerciseEditComponent },
            { path: 'import', component: LogImportComponent },
            { path: 'history', component: LogHistoryComponent },
        ]
    },
];

@NgModule({
    declarations: [
        LogListComponent,
        LogListComponent,
        LogListHeaderComponent,
        LogExerciseAddComponent,
        LogExercisePickerComponent,
        LogExerciseSetsComponent,
        LogExerciseSetComponent,
        LogExerciseEditComponent,
        LogImportComponent,
        LogHistoryComponent],
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
export class LogsModule { }
