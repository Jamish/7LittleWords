using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7LittleWords
{
    class SevenLittleWords
    {
        private List<string> dictionary;
        private List<string> _tiles;
        public List<string> tiles { 
            get 
            {
                return _tiles;
            }
            set
            {
                _tiles = value.Select(x => x.ToLower()).ToList();
            }
        }

        public SevenLittleWords()
        {
            dictionary = File.ReadAllText("words.txt").Split(new string[] { Environment.NewLine }, System.StringSplitOptions.None).ToList();
        }

        public List<string> Solve(int length)
        {
            var potentialWords = new List<string>();
            potentialWords = Recurse("", tiles, length);
            var realWords = potentialWords.Intersect(dictionary);

            return realWords.ToList();
            
        }

        public void EnterSolution(string word)
        {
            tiles = tiles.Except(TrimTiles(word)).ToList();
        }

        private List<string> Recurse(string word, List<string> remainingTiles, int length)
        {
            word = word.ToLower();
            var results = new List<string>();

            if (word.Length < length)
            {
                //Word has not reached the desired length, so recurse.
                foreach (string tile in remainingTiles)
                {
                    var tiles = new List<string>(remainingTiles);
                    tiles.Remove(tile);
                    results.AddRange(Recurse(word + tile, tiles, length));
                }
            }
            else if (word.Length == length)
            {
                //Word matches the right length; return it.
                results.Add(word);
            }

            return results;
        }

        // Takes in a word and list of choices and returns the choices that were used to build the word.
        private List<string> TrimTiles(string word)
        {
            var tilesToRemove = new List<string>();
            word = word.ToLower();

            bool noMatch = false;
            while (word != "" && !noMatch)
            {
                noMatch = true; //Emergency check
                foreach (string tile in tiles)
                {
                    if (word.StartsWith(tile))
                    {
                        noMatch = false;
                        word = word.Replace(tile, "");
                        tilesToRemove.Add(tile);
                        continue;
                    }
                }
            }

            if (noMatch)
            {
                //Something broke... return empty. Should be null?
                return new List<string>();
            }
            else
            {
                return tilesToRemove;
            }
        }
    }

    class Program
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
