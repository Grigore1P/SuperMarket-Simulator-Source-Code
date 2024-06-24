using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OptionsBtn : MonoBehaviour
{

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private List<GameObject> panelsToCheck;
    //Invoke from "Options" button in player HUD
    public void OpenOptions()
    {
        if(panelsToCheck.Any(e => e.activeInHierarchy))
        {
            return;
        }

        optionsPanel.SetActive(true);

    }
}
