using System;
using System.Configuration;
using Augment;

namespace vyger.Common
{
    public static class WorkoutCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public static double Round(double weight, double increment = 5)
        {
            return Math.Round(weight / increment) * increment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>https://www.exrx.net/Calculators/OneRepMax</remarks>
        public static double OneRepMax(double weight, int reps)
        {
            return weight / (1.0278 - 0.0278 * reps);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>https://www.exrx.net/Calculators/OneRepMax</remarks>
        public static double Prediction(double oneRepMax, int reps)
        {
            switch (reps)
            {
                case 1:
                    return oneRepMax * 1.00;
                case 2:
                    return oneRepMax * 0.95;
                case 3:
                    return oneRepMax * 0.90;
                case 4:
                    return oneRepMax * 0.88;
                case 5:
                    return oneRepMax * 0.86;
                case 6:
                    return oneRepMax * 0.83;
                case 7:
                    return oneRepMax * 0.80;
                case 8:
                    return oneRepMax * 0.78;
                case 9:
                    return oneRepMax * 0.76;
                case 10:
                    return oneRepMax * 0.75;
                case 11:
                    return oneRepMax * 0.72;
                case 12:
                    return oneRepMax * 0.70;
            }

            throw new InvalidOperationException("PlannedWeight only supports reps between 1 and 12");
        }
    }
}
