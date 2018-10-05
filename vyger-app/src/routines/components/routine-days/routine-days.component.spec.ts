import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutineDaysComponent } from './routine-days.component';

describe('RoutineDaysComponent', () => {
  let component: RoutineDaysComponent;
  let fixture: ComponentFixture<RoutineDaysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoutineDaysComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutineDaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
