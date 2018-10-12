import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingComponent } from './loading.component';

describe('LoadingComponent', () =>
{
    let component: LoadingComponent;
    let fixture: ComponentFixture<LoadingComponent>;

    beforeEach(async(() =>
    {
        let options = {
            declarations: [LoadingComponent]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));

    describe('constructor', () =>
    {
        it('should create', () =>
        {
            //  arrange
            fixture = TestBed.createComponent(LoadingComponent);
            component = fixture.componentInstance;
            component.loading = false;
            fixture.detectChanges();
            let root: HTMLElement = fixture.nativeElement;
            let el = root.querySelector("div");
            //  act
            //  assert
            expect(el).toBeFalsy();
            expect(component).toBeTruthy();
        });
    });

    describe('loading', () =>
    {
        beforeEach(() =>
        {
            fixture = TestBed.createComponent(LoadingComponent);
            component = fixture.componentInstance;
        });

        it('should not show', () =>
        {
            //  arrange
            component.loading = false;
            fixture.detectChanges();
            let root: HTMLElement = fixture.nativeElement;
            let el = root.querySelector("div");
            //  act
            //  assert
            expect(el).toBeFalsy();
            expect(component).toBeTruthy();
        });

        it('should show', () =>
        {
            //  arrange
            component.loading = true;
            fixture.detectChanges();
            let root: HTMLElement = fixture.nativeElement;
            let el = root.querySelector("div");
            //  act
            //  assert
            expect(el).toBeTruthy();
            expect(component).toBeTruthy();
        });
    });
});
