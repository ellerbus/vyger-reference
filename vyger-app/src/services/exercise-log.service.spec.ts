import { TestBed } from '@angular/core/testing';
import { ExerciseLogService } from 'src/services/exercise-log.service';

describe('LogsService', () =>
{
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () =>
    {
        const service: ExerciseLogService = TestBed.get(ExerciseLogService);
        expect(service).toBeTruthy();
    });
});
