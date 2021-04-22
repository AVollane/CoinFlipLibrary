using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlipLibrary
{
    /// <summary>
    /// Provides extension methods for generating a random boolean value
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <param name="random"></param>
        /// <returns>Random boolean value</returns>
        internal static bool NextBoolean(this Random random)
        {
            byte value = (byte)random.Next(0, 2);

            return value == 0 ? true : false;
        }
    }
}
