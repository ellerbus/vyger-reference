import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { NavigationComponent } from './navigation/navigation.component';
import { NavigationSignInComponent } from './navigation/components/navigation-signin.component';
import { NavigationLoggedInComponent } from './navigation/components/navigation-loggedin.component';
import { NavigationExercisesComponent } from './navigation/components/navigation-exercises.component';
import { HeadingComponent } from './heading/heading.component';

@NgModule({
    declarations: [
        NavigationComponent,
        NavigationSignInComponent,
        NavigationLoggedInComponent,
        NavigationExercisesComponent,
        HeadingComponent,
    ],
    imports: [
        CommonModule,
        RouterModule
    ],
    exports: [
        NavigationComponent,
        HeadingComponent,
    ]
})
export class ComponentsModule { }
