using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetEventSystemSelected : MonoBehaviour
{
  
    public void SetNullForEventSystem()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
