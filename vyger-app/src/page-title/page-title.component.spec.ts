import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleComponent } from './page-title.component';
import { PageTitleService } from './page-title.service';
import { ExpectedConditions } from 'protractor';

describe('PageTitleComponent', () => {
    let component: PageTitleComponent;
    let fixture: ComponentFixture<PageTitleComponent>;
    let mockPageTitleService: jasmine.SpyObj<PageTitleService>;

    beforeEach(async(() => {
        const pageTitleServiceSpy = jasmine.createSpyObj('PageTitleService', ['getTitle']);

        const options = {
            declarations: [PageTitleComponent],
            providers: [
                { provide: PageTitleService, useValue: pageTitleServiceSpy }
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() => {
        mockPageTitleService = TestBed.get(PageTitleService);
        fixture = TestBed.createComponent(PageTitleComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        //  arrange
        mockPageTitleService.getTitle.and.returnValue('x');
        //  act
        let title = component.getTitle();
        //  assert
        expect(component).toBeTruthy();
        expect(mockPageTitleService.getTitle).toHaveBeenCalled();
        expect(title).toBe('x');
    });
});
