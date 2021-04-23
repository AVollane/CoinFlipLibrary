using CoinFlipLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoinFlipTest
{
    [TestClass]
    public class CoinFlipTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void StartFlippingCoin()
        {
            Random random = new Random();
            CoinFlipper.FlipRepeatedly(random, 9000);
        }
    }
}
