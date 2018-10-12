import { } from 'jasmine';
import { Cycle } from './cycle';
import { CycleInput } from './cycle-input';
import { CycleExercise } from './cycle-exercise';

describe('Cycle', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new Cycle();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(4);
            expect(subject.id[0]).toBe('c');
            expect(subject.name).toBeUndefined();
            expect(subject.sequence).toBeUndefined();
            expect(subject.weeks).toBeUndefined();
            expect(subject.days).toBeUndefined();
            expect(subject.lastLogged).toBeUndefined();
            expect(subject.inputs).toEqual([]);
            expect(subject.exercises).toEqual([]);
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
                inputs: [{}],
                exercises: [{}]
            };
            //  act
            let subject = new Cycle(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.weeks).toBe(source.weeks);
            expect(subject.days).toBe(source.days);
            expect(subject.inputs).toBeDefined();
            expect(subject.inputs.length).toBe(1);
            expect(subject.inputs[0]).toEqual(jasmine.any(CycleInput));
            expect(subject.exercises).toBeDefined();
            expect(subject.exercises.length).toBe(1);
            expect(subject.exercises[0]).toEqual(jasmine.any(CycleExercise));
        });
    });
    describe('compare', () =>
    {
        it('should sort by name', () =>
        {
            //  arrange
            let same = { sequence: 1 };
            let source = [
                new Cycle({ name: 'def', ...same }),
                new Cycle({ name: 'abc', ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Cycle.compare);
            //  assert
            expect(results).toEqual(expected);
        });
        it('should sort by sequence desc', () =>
        {
            //  arrange
            let same = { name: 'abc' };
            let source = [
                new Cycle({ sequence: 1, ...same }),
                new Cycle({ sequence: 2, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Cycle.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

