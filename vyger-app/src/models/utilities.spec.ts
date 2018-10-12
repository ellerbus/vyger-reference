import { } from 'jasmine';
import { utilities } from './utilities';

describe('utilities', () =>
{
    describe('extending objects', () =>
    {
        it('should only extended keys', () =>
        {
            //  arrange
            var a = { a: 'a', b: 'b' };
            var b = { a: 'c', b: 'd' };
            //  act
            utilities.extend(b, a, ['a']);
            //  assert
            expect(b.a).toBe(a.a);
        });
    });
});

