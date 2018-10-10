import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogExerciseSetsComponent } from './log-exercise-sets.component';

describe('LogExerciseSetsComponent', () => {
  let component: LogExerciseSetsComponent;
  let fixture: ComponentFixture<LogExerciseSetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogExerciseSetsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogExerciseSetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
