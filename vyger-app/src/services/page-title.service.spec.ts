import { TestBed } from '@angular/core/testing';
import { Title } from '@angular/platform-browser';
import { PageTitleService } from './page-title.service';


describe('PageTitleService', () =>
{
    let service: PageTitleService;
    let mockTitle: jasmine.SpyObj<Title>;

    beforeEach(() =>
    {
        mockTitle = jasmine.createSpyObj('Title', ['setTitle']);

        let options = {
            providers: [
                { provide: Title, useValue: mockTitle }
            ]
        };

        TestBed.configureTestingModule(options);
    });

    beforeEach(() =>
    {
        service = TestBed.get(PageTitleService);
    });

    describe('constructor', () =>
    {
        it('should create', () =>
        {
            //  arrange
            //  act
            //  assert
            expect(service).toBeTruthy();
            expect(mockTitle.setTitle).toHaveBeenCalledWith('vyger');
        });
    });

    describe('setTitle', () =>
    {
        it('should update Title', () =>
        {
            //  arrange
            //  act
            service.setTitle('abc');
            //  assert
            expect(service).toBeTruthy();

            let lastcall = mockTitle.setTitle.calls.mostRecent();

            expect(mockTitle.setTitle).toHaveBeenCalledTimes(2);
            expect(lastcall.args[0]).toBe('vyger - abc');
        });
    });
});
