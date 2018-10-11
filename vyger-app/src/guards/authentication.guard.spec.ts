import { TestBed, async } from '@angular/core/testing';
import { AuthenticationService } from '../services/authentication.service';
import { AuthenticationGuard } from './authentication.guard';
import { RouterStateSnapshot, Router } from '@angular/router';

describe('AuthenticationGuard', () => {

    function createSpies() {
        let spies = {
            authenticationService: jasmine.createSpyObj('AuthenticationService', ['isSignedIn']),
            router: jasmine.createSpyObj('Router', ['navigateByUrl'])
        };
        return spies;
    }

    beforeEach(async(() => {
        const spies = createSpies();
        let options = {
            providers: [
                { provide: AuthenticationService, useValue: spies.authenticationService },
                { provide: Router, useValue: spies.router }
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    describe('constructor', () => {
        let subject: AuthenticationGuard;

        beforeEach(() => {
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockAuthenticationService: jasmine.SpyObj<AuthenticationService> = TestBed.get(AuthenticationService);
            subject = new AuthenticationGuard(mockRouter, mockAuthenticationService);
        });

        it('should instantiate', () => {
            //  arrange
            //  act
            //  assert
            expect(subject).toBeTruthy();
        });
    });

    describe('canActivate', () => {
        let subject: AuthenticationGuard;

        beforeEach(() => {
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockAuthenticationService: jasmine.SpyObj<AuthenticationService> = TestBed.get(AuthenticationService);
            subject = new AuthenticationGuard(mockRouter, mockAuthenticationService);
        });

        it('should follow authenication service', () => {
            //  arrange
            const state: RouterStateSnapshot = <RouterStateSnapshot>{ url: 'x' };
            const mockAuthenticationService: jasmine.SpyObj<AuthenticationService> = TestBed.get(AuthenticationService);

            mockAuthenticationService.isSignedIn.and.returnValue(true);

            //  act
            const results = subject.canActivate(null, state);

            //  assert
            expect(results).toBeTruthy();
        });

        it('should redirect', () => {
            //  arrange
            const state: RouterStateSnapshot = <RouterStateSnapshot>{ url: 'x' };
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockAuthenticationService: jasmine.SpyObj<AuthenticationService> = TestBed.get(AuthenticationService);

            mockAuthenticationService.isSignedIn.and.returnValue(false);

            //  act
            const results = subject.canActivate(null, state);

            //  assert
            expect(results).toBeFalsy();

            const [url, extras] = mockRouter.navigateByUrl.calls.mostRecent().args;
            expect(url).toBe('/home/sign-in');
            expect(extras).toBeDefined();
            expect(extras.queryParams).toBeDefined();
            expect(extras.queryParams.returnUrl).toBe('x');
        });
    });
});
