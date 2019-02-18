import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { SortablejsModule } from 'angular-sortablejs';
import { AppComponent } from 'src/app/app.component';
import { BreadCrumbsComponent } from 'src/common-components/bread-crumbs/bread-crumbs.component';
import { FlashMessagesComponent } from 'src/common-components/flash-messages/flash-messages.component';
import { LoadingComponent } from 'src/common-components/loading/loading.component';
import { PageHeaderComponent } from 'src/common-components/page-header/page-header.component';
import { PageTitleComponent } from 'src/common-components/page-title/page-title.component';
import { ExerciseNameValidatorDirective } from 'src/core-components/exercises/directives/exercise-name-validator.directive';
import { ExerciseCategoryComponent } from 'src/core-components/exercises/exercise-category/exercise-category.component';
import { ExerciseCreateComponent } from 'src/core-components/exercises/exercise-create/exercise-create.component';
import { ExerciseGroupComponent } from 'src/core-components/exercises/exercise-group/exercise-group.component';
import { ExerciseListComponent } from 'src/core-components/exercises/exercise-list/exercise-list.component';
import { ExerciseNameComponent } from 'src/core-components/exercises/exercise-name/exercise-name.component';
import { ExerciseUpdateComponent } from 'src/core-components/exercises/exercise-update/exercise-update.component';
import { HomeComponent } from 'src/core-components/home/home.component';
import { RoutineExerciseDaysPickerComponent } from 'src/core-components/routine-exercise/routine-exercise-days-picker/routine-exercise-days-picker.component';
import { RoutineExerciseListComponent } from 'src/core-components/routine-exercise/routine-exercise-list/routine-exercise-list.component';
import { RoutineExerciseWeeksPickerComponent } from 'src/core-components/routine-exercise/routine-exercise-weeks-picker/routine-exercise-weeks-picker.component';
import { RoutineNameValidatorDirective } from 'src/core-components/routines/directives/routine-name-validator.directive';
import { RoutineCreateComponent } from 'src/core-components/routines/routine-create/routine-create.component';
import { RoutineDaysComponent } from 'src/core-components/routines/routine-days/routine-days.component';
import { RoutineListComponent } from 'src/core-components/routines/routine-list/routine-list.component';
import { RoutineNameComponent } from 'src/core-components/routines/routine-name/routine-name.component';
import { RoutinePatternComponent } from 'src/core-components/routines/routine-pattern/routine-pattern.component';
import { RoutineUpdateComponent } from 'src/core-components/routines/routine-update/routine-update.component';
import { RoutineWeeksComponent } from 'src/core-components/routines/routine-weeks/routine-weeks.component';
import { SignInComponent } from 'src/core-components/sign-in/sign-in.component';
import { CyclesModule } from 'src/cycles/cycles.module';
import { DirectivesModule } from 'src/directives/directives.module';
import { AuthenticationGuard } from 'src/guards/authentication.guard';
import { LogsModule } from 'src/logs/logs.module';
import { AuthenticationService } from 'src/services/authentication.service';
import { FlashMessageService } from 'src/services/flash-message.service';
import { HttpErrorInterceptorService } from 'src/services/http-error-interceptor.service';
import { SideMenuComponent } from 'src/side-menu/side-menu.component';

export function initializeGoogleApi(authenticationService: AuthenticationService)
{
    return () => authenticationService.initializeClient();
}

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    {
        path: 'home',
        children: [
            { path: '', component: HomeComponent, canActivate: [AuthenticationGuard] },
            { path: 'sign-in', component: SignInComponent }
        ]
    },
    {
        path: 'exercises',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: ExerciseListComponent },
            { path: 'create', component: ExerciseCreateComponent },
            { path: 'update/:id', component: ExerciseUpdateComponent }
        ]
    },
    {
        path: 'routines',
        canActivate: [AuthenticationGuard],
        children: [
            { path: '', component: RoutineListComponent },
            { path: 'create', component: RoutineCreateComponent },
            { path: 'update/:id', component: RoutineUpdateComponent },
            { path: 'exercises/:id', component: RoutineExerciseListComponent },
        ]
    },
];

@NgModule({
    declarations: [
        AppComponent,
        //
        //  Common
        //
        SideMenuComponent,
        PageTitleComponent,
        PageHeaderComponent,
        FlashMessagesComponent,
        BreadCrumbsComponent,
        HomeComponent,
        SignInComponent,
        LoadingComponent,
        //
        //  Exercises
        //
        ExerciseListComponent,
        ExerciseCreateComponent,
        ExerciseUpdateComponent,
        ExerciseNameComponent,
        ExerciseGroupComponent,
        ExerciseCategoryComponent,
        ExerciseNameValidatorDirective,
        //
        //  Routines
        //
        RoutineListComponent,
        RoutineCreateComponent,
        RoutineUpdateComponent,
        RoutineNameComponent,
        RoutineDaysComponent,
        RoutineWeeksComponent,
        RoutinePatternComponent,
        RoutineNameValidatorDirective,
        //
        //  Routine Exercises
        //
        RoutineExerciseListComponent,
        RoutineExerciseDaysPickerComponent,
        RoutineExerciseWeeksPickerComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule,
        LogsModule,
        CyclesModule,
        DirectivesModule,
        SortablejsModule.forRoot({ animation: 150 }),
        RouterModule.forRoot(routes, { useHash: true })
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: initializeGoogleApi, multi: true, deps: [AuthenticationService] },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptorService, multi: true, deps: [FlashMessageService] },
        AuthenticationGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
