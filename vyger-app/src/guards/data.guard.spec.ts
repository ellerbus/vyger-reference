import { TestBed, async } from '@angular/core/testing';
import { DataService } from '../services/data.service';
import { DataGuard } from './data.guard';
import { RouterStateSnapshot, Router } from '@angular/router';

describe('DataGuard', () => {

    function createSpies() {
        let spies = {
            dataService: jasmine.createSpyObj('DataService', ['hasUserData']),
            router: jasmine.createSpyObj('Router', ['navigateByUrl'])
        };
        return spies;
    }

    beforeEach(async(() => {
        const spies = createSpies();
        let options = {
            providers: [
                { provide: DataService, useValue: spies.dataService },
                { provide: Router, useValue: spies.router }
            ]
        };

        TestBed.configureTestingModule(options).compileComponents();
    }));

    describe('constructor', () => {
        let subject: DataGuard;

        beforeEach(() => {
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockDataService: jasmine.SpyObj<DataService> = TestBed.get(DataService);
            subject = new DataGuard(mockRouter, mockDataService);
        });

        it('should instantiate', () => {
            //  arrange
            //  act
            //  assert
            expect(subject).toBeTruthy();
        });
    });

    describe('canActivate', () => {
        let subject: DataGuard;

        beforeEach(() => {
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockDataService: jasmine.SpyObj<DataService> = TestBed.get(DataService);
            subject = new DataGuard(mockRouter, mockDataService);
        });

        it('should follow data service', () => {
            //  arrange
            const state: RouterStateSnapshot = <RouterStateSnapshot>{ url: 'x' };
            const mockDataService: jasmine.SpyObj<DataService> = TestBed.get(DataService);

            mockDataService.hasUserData.and.returnValue(true);

            //  act
            const results = subject.canActivate(null, state);

            //  assert
            expect(results).toBeTruthy();
        });

        it('should redirect', () => {
            //  arrange
            const state: RouterStateSnapshot = <RouterStateSnapshot>{ url: 'x' };
            const mockRouter: jasmine.SpyObj<Router> = TestBed.get(Router);
            const mockDataService: jasmine.SpyObj<DataService> = TestBed.get(DataService);

            mockDataService.hasUserData.and.returnValue(false);

            //  act
            const results = subject.canActivate(null, state);

            //  assert
            expect(results).toBeFalsy();

            const [url, extras] = mockRouter.navigateByUrl.calls.mostRecent().args;
            expect(url).toBe('/home/data?returnUrl=x');
        });
    });
});
