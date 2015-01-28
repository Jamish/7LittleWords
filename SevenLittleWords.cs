using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class SevenLittleWords
{
    private List<string> dictionary;
    private List<string> _tiles;
    public List<string> tiles
    {
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