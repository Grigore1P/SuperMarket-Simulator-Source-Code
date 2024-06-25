using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NintendoCanvas : MonoBehaviour
{
   public static NintendoCanvas Instance;
   public GameObject loadingScreen;


    private void Awake()
    {
        Instance = this;
    }
}
