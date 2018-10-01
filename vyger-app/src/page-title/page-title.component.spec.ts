import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleComponent } from './page-title.component';
import { PageTitleService } from './page-title.service';

describe('PageTitleComponent', () => {
    let component: PageTitleComponent;
    let fixture: ComponentFixture<PageTitleComponent>;
    let mockPageTitleService: { title: string };//jasmine.SpyObj<PageTitleService>;

    beforeEach(async(() => {
        const pageTitleServiceSpy = { title: "title" };//jasmine.createSpyObj('PageTitleService', ['methods']);

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
        expect(component).toBeTruthy();
    });

    it('should follow PageTitleService.title', () => {
        //  arrange
        mockPageTitleService.title = 'hello';
        //  act
        let title = component.getTitle();
        //  assert
        expect(title).toBe('hello');
    });
});
