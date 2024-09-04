using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class WordEntryUI : MonoBehaviour
{
    [SerializeField] PuzzleSolver solver;
    public static List<string> solution;
    public GameObject[] entryFields = new GameObject[8];
    public bool editingText = false; // todo remove this when not needed

    public string startWord = "";
    public string endWord = "";

    public void Start()
    {
        entryFields[0].GetComponent<TMP_InputField>().Select();
    }

    public void ControlTextEntry(GameObject parent) {
        int fieldIndex = -1;
        for (int i = 0; i < entryFields.Length; i++) {
            if (GameObject.ReferenceEquals(entryFields[i], parent)) {
                fieldIndex = i;
                break;
            }
        }

        if (editingText) {
            return;
        }

        editingText = true;

        int numFieldCharacters = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Length;

        entryFields[fieldIndex].GetComponent<TMP_InputField>().text = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.ToUpper();

        if (numFieldCharacters > 1) {
            string stringTail = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Substring(1, numFieldCharacters - 1);
            entryFields[fieldIndex].GetComponent<TMP_InputField>().text = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Substring(0, 1);
            if (fieldIndex < entryFields.Length - 1) {
                entryFields[fieldIndex+1].GetComponent<TMP_InputField>().text = stringTail;
                entryFields[fieldIndex+1].GetComponent<TMP_InputField>().Select();
                entryFields[fieldIndex+1].GetComponent<TMP_InputField>().MoveToEndOfLine(false, true);
            }
        }

        if (numFieldCharacters == 0) {
            if (fieldIndex > 0) {
                entryFields[fieldIndex-1].GetComponent<TMP_InputField>().Select();
            }
        }

        editingText = false;

        // if (entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Length == 1) {
        //     entryFields[fieldIndex].GetComponent<TMP_InputField>().text = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.ToUpper();
        // }

        // if (entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Length > 1) {
        //     entryFields[fieldIndex].GetComponent<TMP_InputField>().text = entryFields[fieldIndex].GetComponent<TMP_InputField>().text.Substring(0, 1);
        // }

        // if (Input.GetKeyDown(KeyCode.Backspace) && fieldIndex > 0) {
        //     entryFields[fieldIndex-1].GetComponent<TMP_InputField>().Select();
        //     return;
        // }

        // if (Input.GetKeyDown(KeyCode.Backspace) && fieldIndex == 0) {
        //     entryFields[fieldIndex].GetComponent<TMP_InputField>().Select();
        //     return;
        // }

        // if (fieldIndex < entryFields.Length - 1) {
        //     entryFields[fieldIndex+1].GetComponent<TMP_InputField>().Select();
        //     return;
        // }
    
    }

    public bool GetWordEntry() {
        // todo use a loop here once working
        string top1 = entryFields[0].GetComponent<TMP_InputField>().text;
        string top2 = entryFields[1].GetComponent<TMP_InputField>().text;
        string top3 = entryFields[2].GetComponent<TMP_InputField>().text;
        string top4 = entryFields[3].GetComponent<TMP_InputField>().text;
        string bot1 = entryFields[4].GetComponent<TMP_InputField>().text;
        string bot2 = entryFields[5].GetComponent<TMP_InputField>().text;
        string bot3 = entryFields[6].GetComponent<TMP_InputField>().text;
        string bot4 = entryFields[7].GetComponent<TMP_InputField>().text;

        bool ret = ValidateTextEntry(top1);
        ret &= ValidateTextEntry(top2);
        ret &= ValidateTextEntry(top3);
        ret &= ValidateTextEntry(top4);
        ret &= ValidateTextEntry(bot1);
        ret &= ValidateTextEntry(top2);
        ret &= ValidateTextEntry(top3);
        ret &= ValidateTextEntry(top4);

        if (ret) {
            startWord = top1 + top2 + top3 + top4;
            endWord = bot1 + bot2 + bot3 + bot4;
            Debug.Log("start word: " + startWord + ", end word: " + endWord);
            return true;
        }
        else {
            Debug.Log("Text input is invalid");
            return false;
        }

    }

    public void PuzzleSolveButtonCallback() {
        GetWordEntry();
        solution = solver.SolvePuzzle(startWord, endWord);
    }

    bool ValidateTextEntry(string enteredText) {
        
        // if (enteredText.Length != 1) {
        //     return false;
        // }

        if (!Regex.IsMatch(enteredText, @"^[a-zA-Z]+$")) {
            return false;
        }

        return true;
    }
}