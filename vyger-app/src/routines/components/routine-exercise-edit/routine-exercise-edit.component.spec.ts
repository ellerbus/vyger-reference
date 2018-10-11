import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutineExerciseEditComponent } from './routine-exercise-edit.component';

describe('RoutineExerciseEditComponent', () => {
  let component: RoutineExerciseEditComponent;
  let fixture: ComponentFixture<RoutineExerciseEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoutineExerciseEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutineExerciseEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
