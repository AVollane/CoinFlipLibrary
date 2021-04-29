using System;
using System.Linq;
using System.IO;
using System.Text.Json;
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

            GetModeFromUser();
        }

        static void GetDataFromUserAndStartFlipping()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Number of flips: ");
            string numberOfFlipsString = Console.ReadLine();

            ulong numberOfFlips = 0;
            bool isAsync = false;
            bool[] results = null;
            CoinFlipResults coinFlipResults = null;
            if(UInt64.TryParse(numberOfFlipsString, out numberOfFlips))
            {
                bool result = false;
                Random random = new Random();
                if (numberOfFlips > 1)
                {
                    GetVariantOfFlippingFromUser(ref isAsync);
                    Console.WriteLine($"Asynchronous: {isAsync}");
                    if (isAsync)
                    {
                        results = CoinFlipper.FlipRepeatedlyAsync(random, numberOfFlips).Result;
                    }
                    else
                    {
                        results = CoinFlipper.FlipRepeatedly(random, numberOfFlips);
                    }
                }
                else
                {
                    result = CoinFlipper.Flip(random);
                }

                Console.WriteLine("\nWould you like to save results? y/n");

                string usersAnswerToSaver = Console.ReadLine();

                switch(usersAnswerToSaver){
                    case "y":
                        coinFlipResults = PrepareResults(results);
                        SaveResultsToFile(coinFlipResults);
                        break;
                    case "Y":
                        coinFlipResults = PrepareResults(results);
                        SaveResultsToFile(coinFlipResults);
                        break;
                    default:
                        Console.WriteLine("Ok! Without saving");
                        break;
                }


            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The argument is not in the correct format. Check and enter the value again");
                GetDataFromUserAndStartFlipping();
            }
        }

        static void GetVariantOfFlippingFromUser(ref bool variant){
            Console.WriteLine("\nChoose flipping mode\n1.Not asynchronous\n2.Asynchronous");
            string variantNumberString = Console.ReadLine();

            // Check user's input
            switch(variantNumberString){

                case "1": 
                    Console.WriteLine("Not asynchronous flipping");
                    variant = false;
                    break;

                case "2": 
                    Console.WriteLine("Asynchronous flipping");
                    variant = true;
                    break;

                default:
                    Console.WriteLine("The argument is not in the correct format\n" +
                    "Check and try again");
                    GetVariantOfFlippingFromUser(ref variant);
                    break;
            }
        }

        static CoinFlipResults PrepareResults(bool[] results){

            long tails = results.Where(x => x == true).Count();
            long heads = results.Length - tails;

            return new CoinFlipResults(heads, tails);
        }

        static void SaveResultsToFile(CoinFlipResults result){
            try
            {
                FileStream stream = File.Create(Path.Combine("Results", $"{DateTime.Now}.json"));
                string json = JsonSerializer.Serialize(result);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
                stream.Close();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSaving done!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error! Result isn't saved!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void GetModeFromUser(){
            Console.WriteLine("Enter mode number\n1. Flipping\n2. Take result and make calculations");
            string mode = Console.ReadLine();

            switch(mode){
                case "1":
                    GetDataFromUserAndStartFlipping();
                    break;
                case "2":
                    LoadAndCalculate();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                    GetModeFromUser();
                    break;
            }

        }

        static void LoadAndCalculate(){
            Console.WriteLine("Your results files:");
            try{
                string[] files = Directory.GetFiles("Results");
                for(int i = 0; i < files.Length; i++){
                    Console.WriteLine($"{i + 1}. {files[i]}");
                }
                int fileNumber = GetFileNumberFromUser(files);
                string json = String.Empty;
                using(StreamReader reader = new StreamReader(files[fileNumber - 1])){
                    json = reader.ReadToEnd();
                }

                CoinFlipResults result = JsonSerializer.Deserialize<CoinFlipResults>(json);
                Calculate(result);


            }
            catch(Exception){
                Console.WriteLine("Error!");
                LoadAndCalculate();
            }
        }

        static int GetFileNumberFromUser(string[] files){
            Console.Write("\nEnter saved file number: ");
            string savedFileNumberString = Console.ReadLine();

            int fileNumber = 0;

            if(Int32.TryParse(savedFileNumberString, out fileNumber)){
                if(fileNumber <= files.Length) return fileNumber;
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Out of range! Try again");
                    Console.ForegroundColor = ConsoleColor.White;
                    GetFileNumberFromUser(files);
                }
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid file number! Please try again");
                Console.ForegroundColor = ConsoleColor.White;
                GetFileNumberFromUser(files);
            }

            return 0;
        }

        static void Calculate(CoinFlipResults coinFlipResults){
            ulong all = (ulong)coinFlipResults.Heads + (ulong)coinFlipResults.Tails;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"All: {all}");
            decimal tailsPercentage = Convert.ToDecimal(coinFlipResults.Tails) / 
                Convert.ToDecimal(all);
            decimal headsPercentage = Convert.ToDecimal(coinFlipResults.Heads) / 
                Convert.ToDecimal(all);

            Console.WriteLine($"Tails percentage: {tailsPercentage}\n" + 
                $"Heads percentage: {headsPercentage}");
        }
    }
}
