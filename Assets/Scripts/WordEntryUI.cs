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
    public bool editingText = false;

    public string startWord = "";
    public string endWord = "";

    public void Start()
    {
        SelectField(entryFields[0]);
    }

    public void SelectField(GameObject field) {
        field.GetComponent<TMP_InputField>().Select();
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
    }

    public bool GetWordEntry() {

        for (int i = 0; i < entryFields.Length; i++) {
            string newLetter = entryFields[i].GetComponent<TMP_InputField>().text;
            bool ret = ValidateTextEntry(newLetter);

            if (ret) {
                if (i < (entryFields.Length / 2)) {
                    startWord = startWord + newLetter;
                }
                else {
                    endWord = endWord + newLetter;
                }
            }
            else {
                Debug.Log("Text input is invalid");
                startWord = "";
                endWord = "";
                return false;
            }
        }

        Debug.Log("start word: " + startWord + ", end word: " + endWord);
        return true;
    }

    public void PuzzleSolveButtonCallback() {
        GetWordEntry();
        solution = solver.SolvePuzzle(startWord, endWord);
    }

    bool ValidateTextEntry(string enteredText) {
        
        if (enteredText.Length != 1) {
            return false;
        }

        if (!Regex.IsMatch(enteredText, @"^[a-zA-Z]+$")) {
            return false;
        }

        return true;
    }
}