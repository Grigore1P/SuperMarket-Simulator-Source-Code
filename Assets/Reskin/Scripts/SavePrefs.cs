using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{

    private float timeToSave = 10f;
    private float currentTime = 0;
    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToSave)
        {
            currentTime = 0;
            PlayerPrefs.Save();
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
