import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogListHeaderComponent } from './log-list-header.component';

describe('LogListHeaderComponent', () => {
  let component: LogListHeaderComponent;
  let fixture: ComponentFixture<LogListHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogListHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogListHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
