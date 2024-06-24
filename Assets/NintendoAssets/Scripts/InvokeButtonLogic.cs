using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokeButtonLogic : MonoBehaviour
{
    [SerializeField] private Button btnToPress;
    
    public void PressButton()
    {
        btnToPress.onClick.Invoke();
    }
}
