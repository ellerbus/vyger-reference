import { TestBed } from '@angular/core/testing';

import { RoutinesRepository } from './routines.repository';

describe('RoutinesService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: RoutinesRepository = TestBed.get(RoutinesRepository);
        expect(service).toBeTruthy();
    });
});
