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
        for (int i = 0; i < WordEntryUI.solution.Count; i++) { // todo delete this once working
            GenerateTextLine(WordEntryUI.solution[i]);
            Debug.Log(WordEntryUI.solution[i]);
        }
    }

    private void SetScrolling(GameObject child)
    {

        // todo tidy these up once working

        child.AddComponent(typeof(ScrollRect));
        Transform parent = child.transform.parent;
        ScrollRect parentScroll = parent.GetComponent<ScrollRect>();

        if (parentScroll != null)
        {
            // set scrollrect properties for parent
            child.GetComponent<ScrollRect>().content = parentScroll.content;
            child.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
            child.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
            child.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
            child.GetComponent<ScrollRect>().movementType = parentScroll.movementType;
        }

        // set scrollrect for children
        GameObject letter1 = child.transform.Find("LetterPrefab1").gameObject;
        GameObject letter2 = child.transform.Find("LetterPrefab2").gameObject;
        GameObject letter3 = child.transform.Find("LetterPrefab3").gameObject;
        GameObject letter4 = child.transform.Find("LetterPrefab4").gameObject;
        letter1.AddComponent(typeof(ScrollRect));
        letter1.GetComponent<ScrollRect>().content = parentScroll.content;
        letter1.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
        letter1.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
        letter1.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
        letter1.GetComponent<ScrollRect>().movementType = parentScroll.movementType;

        letter2.AddComponent(typeof(ScrollRect));
        letter2.GetComponent<ScrollRect>().content = parentScroll.content;
        letter2.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
        letter2.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
        letter2.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
        letter2.GetComponent<ScrollRect>().movementType = parentScroll.movementType;

        letter3.AddComponent(typeof(ScrollRect));
        letter3.GetComponent<ScrollRect>().content = parentScroll.content;
        letter3.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
        letter3.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
        letter3.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
        letter3.GetComponent<ScrollRect>().movementType = parentScroll.movementType;

        letter4.AddComponent(typeof(ScrollRect));
        letter4.GetComponent<ScrollRect>().content = parentScroll.content;
        letter4.GetComponent<ScrollRect>().viewport = parentScroll.viewport;
        letter4.GetComponent<ScrollRect>().horizontal = parentScroll.horizontal;
        letter4.GetComponent<ScrollRect>().inertia = parentScroll.inertia;
        letter4.GetComponent<ScrollRect>().movementType = parentScroll.movementType;
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
        SetScrolling(NewInstance);
    }

}
