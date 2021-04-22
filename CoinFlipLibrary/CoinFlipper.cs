using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinFlipLibrary;

namespace CoinFlipLibrary
{
    public static class CoinFlipper
    {
        /// <summary>
        /// Flips a coin once
        /// </summary>
        /// <param name="random"></param>
        /// <returns>Boolean value. True is heads, false is tails</returns>
        public static bool Flip(Random random) => random.NextBoolean();

        /// <summary>
        /// Flips a coin several times asynchronously
        /// </summary>
        /// <param name="random"></param>
        /// <param name="repetitions"></param>
        /// <returns>Array of boolean values. True is heads, false is tails</returns>
        public static async Task<bool[]> FlipRepeatedlyAsync(Random random,long repetitions)
        {
            // Create and run another task
            return await Task.Run(() =>
            {
                // Creates an array of booleans to store the result of a coin flip
                bool[] coinFlipResults = new bool[repetitions];

                // Saves flipping results
                for (int i = 0; i < repetitions; i++)
                    coinFlipResults[i] = Flip(random);

                return coinFlipResults;
            });
        }
        /// <summary>
        /// Flips a coin several times
        /// </summary>
        /// <param name="random"></param>
        /// <param name="repetitions"></param>
        /// <returns>Array of boolean values. True is heads, false is tails</returns>
        public static bool[] FlipRepeatedly(Random random, long repetitions)
        {
            bool[] coinFlipResults = new bool[repetitions];

            for (int i = 0; i < repetitions; i++)
                coinFlipResults[i] = Flip(random);

            return coinFlipResults;
        }
    }
}
