using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] WordEntryUI entry;
    WordList wordlist = new WordList();
    
    public void SolvePuzzle()
    {
        Queue<string> wordqueue = new Queue<string>();
        bool ret = entry.GetWordEntry();

        if (ret == false) {
            return;
        }

        string searchword = entry.startWord.ToLower();
        string targetword = entry.endWord.ToLower();
        string letters = "abcdefghijklmnopqrstuvwxyz"; // todo change this to use regex
        Dictionary<string, int> wordmap = new Dictionary<string, int>();
        Dictionary<string, string> foundmap = new Dictionary<string, string>();
        int dictionarysize = wordlist.dictionarywords.Length;
        for (int wordno = 0; wordno < dictionarysize; wordno++)
        {
            wordmap[wordlist.dictionarywords[wordno]] = wordno;
        }
        wordqueue.Enqueue(searchword);
        while (wordqueue.Count > 0)
        {
            string currentsearchword = wordqueue.Dequeue();
            for (int i = 0; i < currentsearchword.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    char[] newsearchword = currentsearchword.ToCharArray();
                    newsearchword[i] = letters[j];
                    string newsearchwordString = new string(newsearchword);
                    if (newsearchwordString == targetword)
                    {
                        foundmap[newsearchwordString] = currentsearchword;
                        string result = newsearchwordString;
                        while (foundmap[result] != searchword)
                        {
                            Debug.Log(foundmap[result]);
                            result = foundmap[result];
                        }
                        return;
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
            Debug.Log("\n");
        }
        Debug.Log("solution not found");
    }
}


