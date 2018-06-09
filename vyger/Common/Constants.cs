using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Augment;

namespace vyger.Common
{
    public static class Constants
    {
        #region Roles

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string ActiveMember = "ActiveMember";
        }

        #endregion

        #region Constants

        public const string Title = "vyger";

        public const int MinDays = 1;
        public const int MaxDays = 7;

        public const int MinWeeks = 1;
        public const int MaxWeeks = 9;

        public const int MinCycles = 1;
        public const int MaxCycles = 12;

        public const int MinReps = 1;
        public const int MaxReps = 12;

        public const int MinWeight = 1;
        public const int MaxWeight = 9999;

        public static string GetMemberFolder(string email)
        {
            StringBuilder hash = new StringBuilder();

            if (email.IsNotEmpty())
            {
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(email));

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        hash.Append(bytes[i].ToString("x2"));
                    }
                }
            }

            return hash.ToString();
        }

        #endregion

        #region Char Generator

        public static class IdGenerator
        {
            #region Members

            private static RNGCryptoServiceProvider _crypto = new RNGCryptoServiceProvider();

            private const string _alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";

            private static long _lastTick;

            private static readonly Stopwatch _stopwatch = new Stopwatch();

            private static object _lock = new object();

            #endregion

            #region Methods

            /// <summary>
            /// 
            /// </summary>
            /// <param name="size"></param>
            /// <returns></returns>
            public static string Random(int size = 8)
            {
                lock (_lock)
                {
                    byte[] data = new byte[size];

                    _crypto.GetNonZeroBytes(data);

                    char[] chars = new char[size];

                    for (int i = 0; i < size; i++)
                    {
                        chars[i] = _alphabet[data[i] % (_alphabet.Length - 1)];
                    }

                    return new string(chars);
                }
            }

            /// <summary>
            /// Returns a sequential unique id 12 characters in length
            /// (not intended for distributed ID generation)
            /// </summary>
            /// <returns></returns>
            public static string Next()
            {
                lock (_lock)
                {
                    if (DateTime.UtcNow.Ticks > _lastTick)
                    {
                        _stopwatch.Restart();

                        _lastTick = DateTime.UtcNow.Ticks;
                    }

                    _lastTick += _stopwatch.ElapsedTicks;

                    return Encode(_lastTick);
                }
            }

            /// <summary>
            /// Encode the given number into a Base36 string
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            private static string Encode(long input)
            {
                if (input < 0)
                {
                    throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");
                }

                Stack<char> result = new Stack<char>();

                while (input != 0)
                {
                    int index = (int)(input % _alphabet.Length);

                    result.Push(_alphabet[index]);

                    input /= _alphabet.Length;
                }

                return new string(result.ToArray());
            }

            #endregion
        }

        #endregion
    }
}
