import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, NgForm } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { ExerciseNameComponent } from './exercise-name.component';
import { Exercise } from 'src/models/exercise';

describe('ExerciseNameComponent', () =>
{
    let component: ExerciseNameComponent;
    let fixture: ComponentFixture<ExerciseNameComponent>;

    beforeEach(async(() =>
    {
        const options = {
            declarations: [ExerciseNameComponent],
            imports: [FormsModule],
            providers: [NgForm],
            schemas: [NO_ERRORS_SCHEMA]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() =>
    {
        fixture = TestBed.createComponent(ExerciseNameComponent);
        component = fixture.componentInstance;
        component.exercise = new Exercise();
        fixture.detectChanges();
    });

    it('should create', () =>
    {
        expect(component).toBeTruthy();
    });
});
