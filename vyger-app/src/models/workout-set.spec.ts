import { WorkoutSet, WorkoutSetTypes } from './workout-set';

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
        });
    });

    describe('rep max', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RMx5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RMx5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
        });
    });

    describe('rep max with percents', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
        });
    });

    describe('rep max with percents using dashes', () =>
    {
        it('should parse', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM-95%');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(1);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with reps', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM-95%x5');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(1);
        });

        it('should parse with repeat', () =>
        {
            //  arrange
            //  act
            let subject = new WorkoutSet('3RM-95%x5x3');
            //  assert
            expect(subject.type).toBe(WorkoutSetTypes.RepMax);
            expect(subject.repmax).toBe(3);
            expect(subject.percent).toBe(0.95);
            expect(subject.reps).toBe(5);
            expect(subject.repeat).toBe(3);
        });
    });
});
