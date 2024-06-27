using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerGenUI : MonoBehaviour
{

    public GameObject EntryPanelPrefab;
    public GameObject WordEntryPanel;

    public void GenerateTextLine() {
        GameObject NewInstance = Instantiate(EntryPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        NewInstance.transform.SetParent(WordEntryPanel.transform, false);
    }

}
