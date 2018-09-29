import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComponentsModule } from '../components/components.module';
import { HomePageComponent } from './home/home.page';
import { DataPageComponent } from './data/data.page';
import { SignInPageComponent } from './signin/signin.page';
import { ExercisesPageComponent } from './exercises/exercises.page';
import { ExerciseListComponent } from './exercises/components/exercise-list.component';

@NgModule({
    declarations: [
        HomePageComponent,
        DataPageComponent,
        SignInPageComponent,
        ExercisesPageComponent,
        ExerciseListComponent
    ],
    imports: [
        CommonModule,
        ComponentsModule
    ],
    exports: [
        HomePageComponent,
        DataPageComponent,
        SignInPageComponent,
        ExercisesPageComponent
    ]
})
export class PagesModule { }
