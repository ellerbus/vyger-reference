import { } from 'jasmine';
import { Categories, Groups } from './exercise';
import { ExerciseLog } from './exercise-log';

describe('ExerciseLog', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new ExerciseLog();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
            expect(subject.sequence).toBe(1);
            expect(subject.oneRepMax).toBeUndefined();
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
                sets: ['abc']
            };
            //  act
            let subject = new ExerciseLog(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
            expect(subject.sequence).toBe(source.sequence);
            expect(subject.oneRepMax).toBe(source.oneRepMax);
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
                new ExerciseLog({ ymd: 'b', ...same }),
                new ExerciseLog({ ymd: 'a', ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(ExerciseLog.compare);
            //  assert
            expect(results).toEqual(expected);
        });
        it('should sort by sequence', () =>
        {
            //  arrange
            let same = { ymd: 'a', name: 'abc', group: Groups.Abs, category: Categories.Dumbbell };
            let source = [
                new ExerciseLog({ sequence: 2, ...same }),
                new ExerciseLog({ sequence: 1, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(ExerciseLog.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

