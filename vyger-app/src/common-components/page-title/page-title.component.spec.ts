import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleComponent } from './page-title.component';
import { PageTitleService } from './page-title.service';
import { ExpectedConditions } from 'protractor';

describe('PageTitleComponent', () =>
{
    let component: PageTitleComponent;
    let fixture: ComponentFixture<PageTitleComponent>;
    let mockPageTitleService: jasmine.SpyObj<PageTitleService>;

    beforeEach(async(() =>
    {
        let pageTitleServiceSpy = jasmine.createSpyObj('PageTitleService', ['getTitle', 'getSubTitle']);

        let options = {
            declarations: [PageTitleComponent],
            providers: [
                { provide: PageTitleService, useValue: pageTitleServiceSpy }
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    describe('constructor', () =>
    {
        it('should create title only', () =>
        {
            //  arrange
            mockPageTitleService = TestBed.get(PageTitleService);
            mockPageTitleService.getTitle.and.returnValue('x');
            fixture = TestBed.createComponent(PageTitleComponent);
            component = fixture.componentInstance;
            fixture.detectChanges();

            let root: HTMLElement = fixture.nativeElement;
            let h1 = root.querySelector('h1');
            let h2 = root.querySelector('h2');
            //  act
            //  assert
            expect(component).toBeTruthy();
            expect(mockPageTitleService.getTitle).toHaveBeenCalled();
            expect(h1).toBeTruthy();
            expect(h1.textContent).toContain('x');
            expect(h2).toBeFalsy();
        });
        it('should create title with sub-title', () =>
        {
            //  arrange
            mockPageTitleService = TestBed.get(PageTitleService);
            mockPageTitleService.getTitle.and.returnValue('x');
            mockPageTitleService.getSubTitle.and.returnValue('y');
            fixture = TestBed.createComponent(PageTitleComponent);
            component = fixture.componentInstance;
            fixture.detectChanges();

            let root: HTMLElement = fixture.nativeElement;
            let h1 = root.querySelector('h1');
            let h2 = root.querySelector('h2');
            //  act
            //  assert
            expect(component).toBeTruthy();
            expect(mockPageTitleService.getTitle).toHaveBeenCalled();
            expect(h1).toBeTruthy();
            expect(h1.textContent).toContain('x');
            expect(h2).toBeTruthy();
            expect(h2.textContent).toContain('y');
        });
    });
});
