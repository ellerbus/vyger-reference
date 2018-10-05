import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { ExerciseGroupComponent } from './exercise-group.component';
import { Exercise } from '../../../models/exercise';

describe('ExerciseGroupComponent', () => {
    let component: ExerciseGroupComponent;
    let fixture: ComponentFixture<ExerciseGroupComponent>;

    beforeEach(async(() => {
        const options = {
            declarations: [ExerciseGroupComponent],
            imports: [
                FormsModule]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExerciseGroupComponent);
        component = fixture.componentInstance;
        component.exercise = new Exercise();
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
        expect(component.groups).toBeDefined();
    });
});
