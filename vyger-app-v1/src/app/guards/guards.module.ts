
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationGuard } from './authentication.guard';
import { DataGuard } from './data.guard';

@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ],
    providers: [
        AuthenticationGuard,
        DataGuard
    ],
    exports: [
    ]
})
export class GuardsModule { }
