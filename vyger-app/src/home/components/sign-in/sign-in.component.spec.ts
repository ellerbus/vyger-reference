import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleService } from 'src/page-title/page-title.service';
import { SignInComponent } from './sign-in.component';

describe('SignInComponent', () => {
    let component: SignInComponent;
    let fixture: ComponentFixture<SignInComponent>;
    let mockPageTitleService: jasmine.SpyObj<PageTitleService>;

    beforeEach(async(() => {
        const pageTitleServiceSpy = jasmine.createSpyObj('PageTitleService', ['setTitle']);

        const options = {
            declarations: [SignInComponent],
            providers: [
                { provide: PageTitleService, useValue: pageTitleServiceSpy }
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() => {
        mockPageTitleService = TestBed.get(PageTitleService);
        fixture = TestBed.createComponent(SignInComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
