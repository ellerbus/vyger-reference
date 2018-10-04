import { TestBed } from '@angular/core/testing';

import { ExercisesRepository } from './exercises.repository';

describe('ExercisesService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: ExercisesRepository = TestBed.get(ExercisesRepository);
        expect(service).toBeTruthy();
    });
});
