import { } from 'jasmine';
import { CycleGenerator } from './cycle-generator';
import { Cycle } from './cycle';

describe('CycleGenerator', () =>
{
    describe('generate', () =>
    {
        it('should not calculate static', () =>
        {
            //  arrange
            let source = {
                name: 'static',
                sequence: 1,
                weeks: 1,
                days: 1,
                inputs: [{ id: 'x', weight: 100, reps: 5 }],
                exercises: [{ id: 'x', week: 1, day: 1, sets: ['85x5'] }]
            };
            let cycle = new Cycle(source);
            let gen = new CycleGenerator(cycle);
            //  act
            gen.generate();
            //  assert
            expect(cycle.exercises[0].plan).toEqual(['85x5']);
        });

        it('should calculate repmax', () =>
        {
            //  arrange
            let source = {
                name: 'repmax',
                sequence: 1,
                weeks: 1,
                days: 1,
                inputs: [{ id: 'x', weight: 100, reps: 1 }],
                exercises: [{ id: 'x', week: 1, day: 1, sets: ['1RMx5'] }]
            };
            let cycle = new Cycle(source);
            let gen = new CycleGenerator(cycle);
            //  act
            gen.generate();
            //  assert
            expect(cycle.exercises[0].plan).toEqual(['100x5']);
        });

        it('should calculate reference', () =>
        {
            //  arrange
            let source = {
                name: 'reference',
                sequence: 1,
                weeks: 1,
                days: 1,
                inputs: [{ id: 'x', weight: 100, reps: 1 }],
                exercises: [{ id: 'x', week: 1, day: 1, sets: ['[L]-90%x5', '1RMx5'] }]
            };
            let cycle = new Cycle(source);
            let gen = new CycleGenerator(cycle);
            //  act
            gen.generate();
            //  assert
            expect(cycle.exercises[0].plan).toEqual(['90x5', '100x5']);
        });

        it('should calculate multiple weeks', () =>
        {
            //  arrange
            let source = {
                name: 'reference',
                sequence: 1,
                weeks: 1,
                days: 1,
                inputs: [{ id: 'x', weight: 100, reps: 1 }],
                exercises: [
                    { id: 'x', week: 1, day: 1, sets: ['1RMx5'] },
                    { id: 'x', week: 2, day: 1, sets: ['1RMx10'] },
                    { id: 'y', week: 1, day: 1, sets: ['100x15'] }]
            };
            let cycle = new Cycle(source);
            let gen = new CycleGenerator(cycle);
            //  act
            gen.generate();
            //  assert
            expect(cycle.exercises[0].plan).toEqual(['100x5']);
            expect(cycle.exercises[1].plan).toEqual(['100x10']);
            expect(cycle.exercises[2].plan).toEqual(['100x15']);
        });
    });
});

