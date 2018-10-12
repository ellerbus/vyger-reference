import { } from 'jasmine';
import { LogDate, LogDay } from './log-date';

describe('LogDay', () =>
{
    describe('constructor', () =>
    {
        it('should construct', () =>
        {
            //  arrange
            let dt = new Date(2018, 0);
            //  act
            let subject = new LogDay(1, dt);
            //  assert
            expect(subject.index).toBe(1);
            expect(subject.date).toBe(dt);
            expect(subject.full).toBe('Monday');
            expect(subject.short).toBe('M');
            expect(subject.ymd).toBe('2018-01-01');
            expect(subject.active).toBeFalsy();
        });
    });
});

describe('LogDate', () =>
{
    describe('constructor', () =>
    {
        it('should work when empty', () =>
        {
            //  arrange
            let dt = new Date(2018, 0);
            //  act
            let subject = new LogDate(dt);
            //  assert
            expect(subject.date).toBe(dt);
            expect(subject.week).toBe('2017-12-31');
            expect(subject.day).toBe(dt.getDay());
            expect(subject.days).toBeDefined();
            expect(subject.days.length).toBe(7);
        });
    });
});

