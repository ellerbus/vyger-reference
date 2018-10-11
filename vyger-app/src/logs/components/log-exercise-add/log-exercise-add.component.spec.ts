import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogExerciseAddComponent } from './log-exercise-add.component';

describe('LogExerciseAddComponent', () => {
  let component: LogExerciseAddComponent;
  let fixture: ComponentFixture<LogExerciseAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogExerciseAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogExerciseAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
