import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { ExerciseNameComponent } from './exercise-name.component';
import { Exercise } from '../../models/exercise';

describe('ExerciseNameComponent', () => {
    let component: ExerciseNameComponent;
    let fixture: ComponentFixture<ExerciseNameComponent>;

    beforeEach(async(() => {
        const options = {
            declarations: [ExerciseNameComponent],
            imports: [
                FormsModule]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExerciseNameComponent);
        component = fixture.componentInstance;
        component.exercise = new Exercise();
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
