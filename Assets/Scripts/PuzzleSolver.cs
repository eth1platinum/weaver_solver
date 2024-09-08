using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    WordList wordlist = new WordList();

    public void InitWordList(Dictionary<string, int> wordmap) {
        int dictionarysize = wordlist.dictionarywords.Length;
        for (int wordno = 0; wordno < dictionarysize; wordno++)
        {
            wordmap[wordlist.dictionarywords[wordno]] = wordno;
        }
    }
    
    public List<string> SolvePuzzle(string searchword, string targetword)
    {
        Queue<string> wordqueue = new Queue<string>();

        searchword = searchword.ToLower();
        targetword = targetword.ToLower();
        
        const string letters = "abcdefghijklmnopqrstuvwxyz";
        Dictionary<string, int> wordmap = new Dictionary<string, int>();
        Dictionary<string, string> foundmap = new Dictionary<string, string>();
        List<string> solutionlist = new List<string>();

        InitWordList(wordmap);

        wordqueue.Enqueue(searchword);

        while (wordqueue.Count > 0)
        {
            string currentsearchword = wordqueue.Dequeue();

            for (int searchwordletterindex = 0; searchwordletterindex < currentsearchword.Length; searchwordletterindex++)
            {
                for (int newletterindex = 0; newletterindex < letters.Length; newletterindex++)
                {
                    char[] newsearchword = currentsearchword.ToCharArray();
                    newsearchword[searchwordletterindex] = letters[newletterindex];
                    string newsearchwordstring = new string(newsearchword);

                    if (newsearchwordstring == targetword)
                    { // if the target has been found, traverse the solution list to find shortest path
                        foundmap[newsearchwordstring] = currentsearchword;
                        string result = newsearchwordstring;
                        solutionlist.Add(targetword);
                        while (foundmap[result] != searchword)
                        {
                            solutionlist.Add(foundmap[result]);
                            result = foundmap[result];
                        }
                        solutionlist.Add(searchword);
                        solutionlist.Reverse();
                        return solutionlist;
                    }
                    bool wordexists = false;
                    if (wordmap.ContainsKey(newsearchwordstring))
                    { // if word is valid but is not the target word
                        wordexists = true;
                    }
                    bool wordalreadyfound = false;
                    if (foundmap.ContainsKey(newsearchwordstring))
                    { // if word has been found previously (in less or equal number of moves)
                        wordalreadyfound = true;
                    }
                    if (!wordexists || newsearchwordstring == currentsearchword)
                    { // if word is invalid or the same as the previous word then ignore
                        continue;
                    }
                    else if (wordexists && wordalreadyfound)
                    { // if word exists and has already been found (todo duplicate of above?)
                        continue;
                    }
                    else
                    { // if word is valid and not already found then add to foundmap as key with previous word as value
                        foundmap[newsearchwordstring] = currentsearchword;
                        wordqueue.Enqueue(newsearchwordstring);
                    }
                }
            }
        }
        Debug.Log("solution not found");
        return solutionlist;
    }
}


