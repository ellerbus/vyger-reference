import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { FlashMessageService } from 'src/services/flash-message.service';
import { Observable } from 'rxjs';

describe('AppComponent', () =>
{
    class MockRouter
    {
        public ne = new NavigationEnd(0, 'http://localhost:4200/login', 'http://localhost:4200/login');
        public events = new Observable(observer =>
        {
            observer.next(this.ne);
            observer.complete();
        });
    }

    let mockRouter: MockRouter;
    let mockFlashMessageService: jasmine.SpyObj<FlashMessageService>;

    beforeEach(async(() =>
    {
        mockRouter = new MockRouter();
        mockFlashMessageService = jasmine.createSpyObj('FlashMessageService', ['clean']);

        let options = {
            declarations: [AppComponent],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [
                { provide: FlashMessageService, useValue: mockFlashMessageService },
                { provide: Router, useValue: mockRouter }
            ]
        };
        TestBed.configureTestingModule(options).compileComponents();
    }));
    it('should create the app', async(() =>
    {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
});
