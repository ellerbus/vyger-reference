import { } from 'jasmine';
import { Exercise, Groups, Categories } from './exercise';

describe('Exercise', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new Exercise();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
        });
        it('should extend source', () =>
        {
            //  arrange
            let source = {
                id: 'x',
                name: 'abc',
                group: Groups.Abs,
                category: Categories.Barbell
            };
            //  act
            let subject = new Exercise(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
        });
    });
    describe('compare', () =>
    {
        it('should sort by name', () =>
        {
            //  arrange
            let same = { group: Groups.Abs, category: Categories.Barbell };
            let source = [
                new Exercise({ name: 'def', ...same }),
                new Exercise({ name: 'abc', ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Exercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
        it('should sort by group', () =>
        {
            //  arrange
            let same = { name: 'abc', category: Categories.Barbell };
            let source = [
                new Exercise({ group: Groups.Biceps, ...same }),
                new Exercise({ group: Groups.Abs, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Exercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
        it('should sort by category', () =>
        {
            //  arrange
            let same = { name: 'abc', group: Groups.Abs };
            let source = [
                new Exercise({ category: Categories.Dumbbell, ...same }),
                new Exercise({ category: Categories.Barbell, ...same }),
            ];
            let expected = [source[1], source[0]];
            //  act
            let results = source.sort(Exercise.compare);
            //  assert
            expect(results).toEqual(expected);
        });
    });
});

