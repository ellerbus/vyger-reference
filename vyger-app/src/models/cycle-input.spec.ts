import { } from 'jasmine';
import { CycleInput } from './cycle-input';
import { Groups, Categories } from './exercise';

describe('CycleInput', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new CycleInput();
            //  assert
            expect(subject.id).toBeDefined();
            expect(subject.id.length).toBe(3);
            expect(subject.id[0]).toBe('x');
            expect(subject.name).toBeUndefined();
            expect(subject.group).toBeUndefined();
            expect(subject.category).toBeUndefined();
            expect(subject.weight).toBeUndefined();
            expect(subject.reps).toBe(5);
            expect(subject.pullback).toBe(0);
            expect(subject.requiresInput).toBeUndefined(0);
        });
        it('should extend source', () =>
        {
            //  arrange
            let source = {
                id: 'x',
                name: 'abc',
                group: Groups.Abs,
                category: Categories.Barbell,
                weight: 9,
                reps: 99,
                pullback: 999,
                requiresInput: true
            };
            //  act
            let subject = new CycleInput(source);
            //  assert
            expect(subject.id).toBe(source.id);
            expect(subject.name).toBe(source.name);
            expect(subject.group).toBe(source.group);
            expect(subject.category).toBe(source.category);
            expect(subject.weight).toBe(source.weight);
            expect(subject.reps).toBe(source.reps);
            expect(subject.pullback).toBe(source.pullback);
            expect(subject.requiresInput).toBe(source.requiresInput);
        });
    });
});

