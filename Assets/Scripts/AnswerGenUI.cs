using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        for (int i = 0; i < WordEntryUI.solution.Count; i++) {
            GenerateTextLine(WordEntryUI.solution[i]);
        }
    }

    private void SetScrolling(GameObject child)
    {
        child.AddComponent(typeof(ScrollRect));
        Transform parent = child.transform.parent;
        ScrollRect parentScroll = parent.GetComponent<ScrollRect>();

        // set scrollrect properties for parent

        if (parentScroll != null)
        {
            child.GetComponent<ScrollRect>().content = parentScroll.content;
            child.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
            child.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
            child.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
            child.GetComponent<ScrollRect>().movementType = parentScroll.movementType;
        }

        // copy parent scrollrect properties to children

        for (int i = 0; i < child.transform.childCount; i++) {
            GameObject letter = child.transform.Find("LetterPrefab" + (i+1)).gameObject;

            letter.AddComponent(typeof(ScrollRect));
            letter.GetComponent<ScrollRect>().content = parentScroll.content;
            letter.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
            letter.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
            letter.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
            letter.GetComponent<ScrollRect>().movementType = parentScroll.movementType;
            letter.GetComponent<TMP_InputField>().textViewport = parentScroll.viewport;
        }
    }

    void AddWordToTextField(GameObject instance, string word) {
        for (int i = 0; i < word.Length; i++) {
            GameObject letter = instance.transform.Find("LetterPrefab" + (i+1)).gameObject;
            letter.GetComponent<TMP_InputField>().text = word[i].ToString();
        }
    }

    public void GenerateTextLine(string word) {
        GameObject NewInstance = Instantiate(EntryPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        AddWordToTextField(NewInstance, word.ToUpper());
        NewInstance.transform.SetParent(WordEntryPanel.transform, false);
        SetScrolling(NewInstance);
    }

}
