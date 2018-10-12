import { } from 'jasmine';
import { LogExercise } from './log-exercise';
import { Groups, Categories } from './exercise';

describe('LogExercise', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new LogExercise();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
            expect(subject.sequence).toBe(1);
            expect(subject.oneRepMax).toBeUndefined();
            expect(subject.oneRepMaxSet).toBe(0);
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
                oneRepMax: 99,
                oneRepMaxSet: 999,
                sets: ['abc']
            };
            //  act
            let subject = new LogExercise(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
            expect(subject.sequence).toBe(source.sequence);
            expect(subject.oneRepMax).toBe(source.oneRepMax);
            expect(subject.oneRepMaxSet).toBe(source.oneRepMaxSet);
            expect(subject.sets).toEqual(source.sets);
        });
    });
    describe('compare', () =>
    {
        it('should sort by ymd', () =>
        {
            //  arrange
            let same = { name: 'abc', sequence: 1, group: Groups.Abs, category: Categories.Dumbbell };
            let source = [
                new LogExercise({ ymd: 'b', ...same }),
                new LogExercise({ ymd: 'a', ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(LogExercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
        it('should sort by sequence', () =>
        {
            //  arrange
            let same = { ymd: 'a', name: 'abc', group: Groups.Abs, category: Categories.Dumbbell };
            let source = [
                new LogExercise({ sequence: 2, ...same }),
                new LogExercise({ sequence: 1, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(LogExercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

