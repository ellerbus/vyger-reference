import { TestBed } from '@angular/core/testing';
import { ExercisesService } from 'src/services/exercise.service';

describe('ExercisesService', () =>
{
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () =>
    {
        const service: ExercisesService = TestBed.get(ExercisesService);
        expect(service).toBeTruthy();
    });
});
