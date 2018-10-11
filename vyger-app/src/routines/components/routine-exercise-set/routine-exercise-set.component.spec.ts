import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutineExerciseSetComponent } from './routine-exercise-set.component';

describe('RoutineExerciseSetComponent', () => {
  let component: RoutineExerciseSetComponent;
  let fixture: ComponentFixture<RoutineExerciseSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoutineExerciseSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutineExerciseSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
