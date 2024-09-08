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

            for (int searchwordindex = 0; searchwordindex < currentsearchword.Length; searchwordindex++)
            { // index of letter in search word
                for (int newletterindex = 0; newletterindex < letters.Length; newletterindex++)
                { // index of new attempted letter in alphabet (0-25)
                    char[] newsearchword = currentsearchword.ToCharArray();
                    newsearchword[searchwordindex] = letters[newletterindex];
                    string newsearchwordstring = new string(newsearchword);

                    if (newsearchwordstring == targetword)
                    { // if the target has been found, traverse the solution list to find shortest path
                        string result = newsearchwordstring;
                        foundmap[newsearchwordstring] = currentsearchword;
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

                    bool wordexists = wordmap.ContainsKey(newsearchwordstring);
                    bool wordalreadyfound = foundmap.ContainsKey(newsearchwordstring);

                    if (!wordexists || wordalreadyfound || newsearchwordstring == currentsearchword)
                    { // if word is invalid, word was already found or word is the same as the previous word then ignore
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


