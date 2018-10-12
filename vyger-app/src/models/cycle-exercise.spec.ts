import { } from 'jasmine';
import { CycleExercise } from './cycle-exercise';
import { Groups, Categories } from './exercise';

describe('CycleExercise', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new CycleExercise();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
            expect(subject.sequence).toBe(1);
            expect(subject.sets).toEqual([]);
            expect(subject.plan).toEqual([]);
        });
        it('should extend source', () =>
        {
            //  arrange
            let source = {
                id: 'x',
                name: 'abc',
                group: Groups.Abs,
                category: Categories.Barbell,
                sequence: 9,
                sets: ['abc'],
                plan: ['def']
            };
            //  act
            let subject = new CycleExercise(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
            expect(subject.sequence).toBe(source.sequence);
            expect(subject.sets).toEqual(source.sets);
            expect(subject.plan).toEqual(source.plan);
        });
    });
    describe('compare', () =>
    {
        it('should sort by sequence', () =>
        {
            //  arrange
            let same = { ymd: 'a', name: 'abc', group: Groups.Abs, category: Categories.Dumbbell };
            let source = [
                new CycleExercise({ sequence: 2, ...same }),
                new CycleExercise({ sequence: 1, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(CycleExercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

