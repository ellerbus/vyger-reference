import { } from 'jasmine';
import { RoutineExercise } from './routine-exercise';
import { Groups, Categories } from './exercise';

describe('RoutineExercise', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new RoutineExercise();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
            expect(subject.sequence).toBe(1);
            expect(subject.week).toBeUndefined();
            expect(subject.day).toBeUndefined();
            expect(subject.sets).toEqual([]);
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
                week: 99,
                day: 999,
                sets: ['abc']
            };
            //  act
            let subject = new RoutineExercise(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
            expect(subject.sequence).toBe(source.sequence);
            expect(subject.week).toBe(source.week);
            expect(subject.day).toBe(source.day);
            expect(subject.sets).toEqual(source.sets);
        });
    });
    describe('compare', () =>
    {
        it('should sort by sequence', () =>
        {
            //  arrange
            let same = { name: 'abc', group: Groups.Abs, category: Categories.Dumbbell };
            let source = [
                new RoutineExercise({ sequence: 2, ...same }),
                new RoutineExercise({ sequence: 1, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(RoutineExercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

