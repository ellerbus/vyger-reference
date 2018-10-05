import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, NgForm } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { ExerciseGroupComponent } from './exercise-group.component';
import { Exercise } from '../../../models/exercise';

describe('ExerciseGroupComponent', () =>
{
    let component: ExerciseGroupComponent;
    let fixture: ComponentFixture<ExerciseGroupComponent>;

    beforeEach(async(() =>
    {
        const options = {
            declarations: [ExerciseGroupComponent],
            imports: [FormsModule],
            providers: [NgForm],
            schemas: [NO_ERRORS_SCHEMA]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() =>
    {
        fixture = TestBed.createComponent(ExerciseGroupComponent);
        component = fixture.componentInstance;
        component.exercise = new Exercise();
        fixture.detectChanges();
    });

    it('should create', () =>
    {
        expect(component).toBeTruthy();
        expect(component.groups).toBeDefined();
    });
});
