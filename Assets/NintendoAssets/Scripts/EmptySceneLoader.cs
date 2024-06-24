using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EmptySceneLoader : MonoBehaviour
{
    public Image filler;
    public Text Counter;

    WaitForSeconds waitSeconds = new WaitForSeconds(0.1f);
    void Start()
    {
        StartCoroutine(startgame());
    }

    IEnumerator startgame()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(GameKeys.SceneToLoad);
        loadScene.allowSceneActivation = false;

        while (!loadScene.isDone)
        {
            float progressValue = Mathf.Clamp01(loadScene.progress / 0.9f);
            filler.fillAmount = progressValue;

            int textValue = (int)(progressValue * 100);
            Counter.text = "LOADING - " + textValue.ToString() + "%";

            if (loadScene.progress >= 0.9f)
            {
                loadScene.allowSceneActivation = true;
            }       
            yield return waitSeconds;
        }

    }
}
