import { } from 'jasmine';
import { WorkoutSet, WorkoutSetTypes } from './workout-set';
import { worker } from 'cluster';

describe('WorkoutSet', () =>
{
    describe('empty', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('');
            //  assert
            expect(subject.reference).toBe('L');
            expect(subject.weight).toBe(0);
            expect(subject.repmax).toBe(1);
            expect(subject.percent).toBe(100);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
        });
    });

    describe('body weight', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('BW');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.BodyWeight);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('BWx1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('BWx5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.BodyWeight);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('BWx5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('BWx5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.BodyWeight);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('BWx5x3');
        });
    });

    describe('static weight', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('123');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Static);
            expect(subject.weight).toBe(123);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('123x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('123x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Static);
            expect(subject.weight).toBe(123);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('123x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('123x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Static);
            expect(subject.weight).toBe(123);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('123x5x3');
        });
    });

    describe('reference', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]x5x3');
        });
    });

    describe('reference with percents', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]-95%x5x3');
        });
    });

    describe('reference with percents using dashes', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]-95%x5x3');
        });
    });

    describe('reference', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]x5x3');
        });
    });

    describe('reference with percents', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]-95%x5x3');
        });
    });

    describe('reference with percents using dashes', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x1');
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
            expect(subject.pattern).toBe('[L]-95%x5');
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('[L]-95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.Reference);
            expect(subject.reference).toBe('L');
            expect(subject.percent).toBe(95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
            expect(subject.pattern).toBe('[L]-95%x5x3');
        });
    });
});

