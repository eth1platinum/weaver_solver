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
                    string newsearchwordString = new string(newsearchword);
                    if (newsearchwordString == targetword)
                    {
                        foundmap[newsearchwordString] = currentsearchword;
                        string result = newsearchwordString;
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
                    if (wordmap.ContainsKey(newsearchwordString))
                    {
                        wordexists = true;
                    }
                    bool wordalreadyfound = false;
                    if (foundmap.ContainsKey(newsearchwordString))
                    {
                        wordalreadyfound = true;
                    }
                    if (!wordexists || newsearchwordString == currentsearchword)
                    {
                        continue;
                    }
                    else if (wordexists && wordalreadyfound)
                    {
                        continue;
                    }
                    else
                    {
                        foundmap[newsearchwordString] = currentsearchword;
                        wordqueue.Enqueue(newsearchwordString);
                    }
                }
            }
        }
        Debug.Log("solution not found");
        return solutionlist;
    }
}


