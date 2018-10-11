import { TestBed } from '@angular/core/testing';

import { LogsRepository } from './logs.repository';

describe('LogsService', () =>
{
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () =>
    {
        const service: LogsRepository = TestBed.get(LogsRepository);
        expect(service).toBeTruthy();
    });
});
