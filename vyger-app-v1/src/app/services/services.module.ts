import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationService } from './authentication.service';
import { DataService } from './data.service';

@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule
    ],
    providers: [
        AuthenticationService,
        DataService
    ],
    exports: []
})
export class ServicesModule { }
