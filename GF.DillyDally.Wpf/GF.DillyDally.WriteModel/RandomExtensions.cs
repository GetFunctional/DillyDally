using System;

namespace GF.DillyDally.WriteModel
{
    internal static class RandomExtensions
    {
        /// <summary>
        ///     Get a random double, between 0.0 and 1.0.
        /// </summary>
        /// <param name="rng">Randomizer</param>
        /// <param name="min">Minimum, default 0.0</param>
        /// <param name="max">Maximum, default 1.0</param>
        public static double Double(this Random rng, double min = 0.0d, double max = 1.0d)
        {
            if (min == 0.0d && max == 1.0d)
            {
                //use default implementation
                return rng.NextDouble();
            }

            return rng.NextDouble() * (max - min) + min;
        }

        /// <summary>
        ///     Get an int from min to max.
        /// </summary>
        /// <param name="rng">Randomizer</param>
        /// <param name="min">Lower bound, inclusive</param>
        /// <param name="max">Upper bound, inclusive. Only int.MaxValue is exclusive.</param>
        public static int Number(this Random rng, int min = 0, int max = 1)
        {
            //Clamp max value, Issue #30.
            max = max == int.MaxValue ? max : max + 1;
            return rng.Next(min, max);
        }

        /// <summary>
        ///     Get a random decimal, between 0.0 and 1.0.
        /// </summary>
        /// <param name="rng">Randomizer</param>
        /// <param name="min">Minimum, default 0.0</param>
        /// <param name="max">Maximum, default 1.0</param>
        public static decimal Decimal(this Random rng, decimal min = 0.0m, decimal max = 1.0m)
        {
            return Convert.ToDecimal(rng.Double()) * (max - min) + min;
        }

        /// <summary>
        ///     Get a random float, between 0.0 and 1.0.
        /// </summary>
        /// <param name="rng">Randomizer</param>
        /// <param name="min">Minimum, default 0.0</param>
        /// <param name="max">Maximum, default 1.0</param>
        public static float Float(this Random rng, float min = 0.0f, float max = 1.0f)
        {
            return Convert.ToSingle(rng.Double()) * (max - min) + min;
        }

        /// <summary>
        ///     Generate a random byte between 0 and 255.
        /// </summary>
        /// <param name="rng">Randomizer</param>
        /// <param name="min">Min value, default 0</param>
        /// <param name="max">Max value, default 255</param>
        public static byte Byte(this Random rng, byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return Convert.ToByte(rng.Number(min, max));
        }
    }
}