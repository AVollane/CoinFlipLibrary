using System;
using CoinFlipLibrary;

namespace CoinFlipConsole
{
    class Program
    {
        /// <summary>
        /// Entrypoint
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Console shell for a coin flip program by Anton Baryshev\n\n" +
                "To flip a coin and get results, enter the number of flips");
            GetDataFromUserAndStartFlipping();
        }

        static void GetDataFromUserAndStartFlipping()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Number of flips: ");
            string numberOfFlipsString = Console.ReadLine();

            ulong numberOfFlips = 0;
            if(UInt64.TryParse(numberOfFlipsString, out numberOfFlips))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Start flipping!");
                // Здесь нужно добавить вызов метода, вращающего монетку, а потом убрать комментарий
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The argument is not in the correct format. Check and enter the value again");
                GetDataFromUserAndStartFlipping();
            }
        }
    }
}
