using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public void LoadWordEntryUI() {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync("EntryScreen", LoadSceneMode.Single);
    }

    public void LoadAnswerGenUI() {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync("AnswerScreen", LoadSceneMode.Single);
    }

}
