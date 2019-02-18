import { TestBed } from '@angular/core/testing';
import { ExerciseService } from 'src/services/exercise.service';

describe('ExerciseService', () =>
{
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () =>
    {
        const service: ExerciseService = TestBed.get(ExerciseService);
        expect(service).toBeTruthy();
    });
});
