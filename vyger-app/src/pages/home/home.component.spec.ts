import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleService } from 'src/services/page-title.service';
import { AuthenticationService } from 'src/services/authentication.service';
import { HomeComponent } from './home.component';

describe('HomeComponent', () =>
{
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;
    let mockPageTitleService: jasmine.SpyObj<PageTitleService>;
    let mockAuthenticationService: { user: { givenName: string } };// jasmine.SpyObj<PageTitleService>;

    beforeEach(async(() =>
    {
        const pageTitleServiceSpy = jasmine.createSpyObj('PageTitleService', ['setTitle']);
        const authenticationServiceSpy = { user: { givenName: 'x' } };//jasmine.createSpyObj('PageTitleService', ['setTitle']);

        const options = {
            declarations: [HomeComponent],
            providers: [
                { provide: PageTitleService, useValue: pageTitleServiceSpy },
                { provide: AuthenticationService, useValue: authenticationServiceSpy },
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    beforeEach(() =>
    {
        mockPageTitleService = TestBed.get(PageTitleService);
        mockAuthenticationService = TestBed.get(AuthenticationService);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () =>
    {
        expect(component).toBeTruthy();
        expect(mockPageTitleService.setTitle).toHaveBeenCalledWith('Hello x');
    });
});
