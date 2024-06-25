using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SimplePanel : MonoBehaviour
{
    [Header("Can leave to bee empty fields")]
    [SerializeField] private GameObject firstSelected;
    private GameObject lastSelected;
    public bool isOpen;
    public bool isWithHoldLastSelected;
    public bool isAutoSelected;
    public UnityEvent OnPanelOpen = new UnityEvent();
    public UnityEvent OnPanelClose = new UnityEvent();

    private void OnEnable()
    {
        if (isAutoSelected)
        {
            Show();
        }
    }

    private void OnDisable()
    {
        if (isAutoSelected)
        {
            Hide();
        }
    }
    public void Show()
    {

        gameObject.SetActive(true);
        isOpen = true;

        if (isWithHoldLastSelected)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {

             lastSelected = EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                lastSelected = null;
            }
        }
        if (firstSelected)
        {
        EventSystem.current.SetSelectedGameObject(firstSelected);
        }
        OnPanelOpen.Invoke();

    }

    public void Hide()
    {
        OnPanelClose.Invoke();
        gameObject.SetActive(false);
        isOpen = false;

        if (isWithHoldLastSelected)
        {
            if (lastSelected)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
            else
            {

            }
        }
        else
        {
            if(EventSystem.current)
            EventSystem.current.SetSelectedGameObject(null);

        }

    }
}
