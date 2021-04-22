using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlipLibrary
{
    public class CoinFlipResults
    {
        public CoinFlipResults(long heads, long tails)
        {
            Heads = heads;
            Tails = tails;
        }

        public long Heads { get; }

        public long Tails { get; }
    }
}
