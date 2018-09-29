import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeadingComponent } from './heading.component';

describe('HeadingComponent', () => {

    beforeEach(async(() => {
        let options = {
            declarations: [HeadingComponent],
            providers: [
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    it('should be created', () => {
        //  arrange
        const fixture = TestBed.createComponent(HeadingComponent);
        const component = fixture.componentInstance;
        fixture.detectChanges();
        //  act
        //  assert
        expect(component).toBeTruthy();
    });

    it('should display heading', () => {
        //  arrange
        const fixture = TestBed.createComponent(HeadingComponent);
        const component = fixture.componentInstance;
        component.heading = 'abc';

        fixture.detectChanges();

        const heading: HTMLElement = fixture.nativeElement.querySelector('h3');
        //  act
        //  assert
        expect(heading.textContent).toBe('abc');
    });
});
