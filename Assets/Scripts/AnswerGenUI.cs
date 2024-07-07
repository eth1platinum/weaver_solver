using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerGenUI : MonoBehaviour
{

    public GameObject EntryPanelPrefab;
    public GameObject WordEntryPanel;

    void OnEnable()
    {
        LoadSolution();
    }

    void LoadSolution() {
        for (int i = 0; i < WordEntryUI.solution.Count; i++) { // todo delete this once working
            GenerateTextLine(WordEntryUI.solution[i]);
            Debug.Log(WordEntryUI.solution[i]);
        }
    }

    void AddWordToTextField(GameObject instance, string word) { // todo tidy these up once working
        GameObject letter1 = instance.transform.Find("LetterPrefab1").gameObject;
        GameObject letter2 = instance.transform.Find("LetterPrefab2").gameObject;
        GameObject letter3 = instance.transform.Find("LetterPrefab3").gameObject;
        GameObject letter4 = instance.transform.Find("LetterPrefab4").gameObject;
        letter1.GetComponent<TMP_InputField>().text = word[0].ToString();
        letter2.GetComponent<TMP_InputField>().text = word[1].ToString();
        letter3.GetComponent<TMP_InputField>().text = word[2].ToString();
        letter4.GetComponent<TMP_InputField>().text = word[3].ToString();
    }

    public void GenerateTextLine(string word) {
        GameObject NewInstance = Instantiate(EntryPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        AddWordToTextField(NewInstance, word.ToUpper());
        NewInstance.transform.SetParent(WordEntryPanel.transform, false);

    }

}
