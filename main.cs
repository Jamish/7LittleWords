using System;
using System.Collections.Generic;
using System.Text;

namespace _7LittleWords
{
    class main
    {
        static void Main(string[] args)
        {
            SevenLittleWords slw = new SevenLittleWords();

            
            Console.WriteLine("Enter each of the 20 tiles (hit 'return' between each one). When finished, hit return (with a blank line) to continue.\n");

            var tiles = new List<string>();
            string input = null;
            while (input != "")
            {
                Console.Write("> ");
                input = Console.ReadLine();
                if (input.Length > 1) 
                {
                    tiles.Add(input);
                }
            }

            slw.tiles = tiles;

            
            Console.Clear();
            int length = 0;
            while (true)
            {
                Console.WriteLine("Type a number to see the possible words (e.g., '5' will show all 5 letter solutions.)");
                Console.WriteLine("Or, type a completed word to eliminate some choices.");
                Console.WriteLine("Or, type 'exit' to quit.\n");
                Console.Write("> ");
                input = Console.ReadLine();

                if (input == "exit")
                {
                    System.Environment.Exit(1);
                }

                length = 0;
                if (Int32.TryParse(input, out length))
                {
                    Console.WriteLine("Calculating...");
                    var potentialSolutions = slw.Solve(length);
                    if (potentialSolutions.Count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Potential solutions:");
                        potentialSolutions.ForEach(x => Console.WriteLine(x));
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(String.Format("No solutions found of length {0}!\n", length));
                    }
                        
                        
                }
                else //Entered a solution, not a length
                {
                    slw.EnterSolution(input.Trim());

                    Console.Clear();
                    Console.WriteLine("Eliminated some tiles. There should be better solutions now!\n");
                }
            }
        }

        

        
    }
}
