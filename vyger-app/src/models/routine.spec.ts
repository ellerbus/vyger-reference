import { } from 'jasmine';
import { Routine } from './routine';
import { RoutineExercise } from './routine-exercise';

describe('Routine', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new Routine();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('r');
            expect(subject.name).toBeUndefined();
            expect(subject.weeks).toBe(4);
            expect(subject.days).toBe(3);
            expect(subject.exercises).toEqual([]);
            expect(subject.sets).toEqual([]);
        });
        it('should extend source', () =>
        {
            //  arrange
            let source = {
                id: 'x',
                name: 'abc',
                weeks: 9,
                days: 99,
                sets: ['abc'],
                exercises: [{}]
            };
            //  act
            let subject = new Routine(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.weeks).toBe(source.weeks);
            expect(subject.days).toBe(source.days);
            expect(subject.exercises).toBeDefined();
            expect(subject.exercises.length).toBe(1);
            expect(subject.exercises[0]).toEqual(jasmine.any(RoutineExercise));
            expect(subject.sets).toEqual(source.sets);
        });
    });
    describe('compare', () =>
    {
        it('should sort by name', () =>
        {
            //  arrange
            let source = [
                new Routine({ name: 'def' }),
                new Routine({ name: 'abc' }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Routine.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

