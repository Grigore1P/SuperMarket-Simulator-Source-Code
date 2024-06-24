using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMove : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform parentReference;
    [SerializeField] private bool isVerticalScroller;

    RectTransform selectedChild;
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }


        if (EventSystem.current.currentSelectedGameObject.transform.parent == parentReference)
        {
            // Get the selected child's position
            selectedChild = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>(); // this is default line 

            RectTransform content = scrollRect.content;

            if (isVerticalScroller)
            {

                float selectedChildY = Mathf.Abs(selectedChild.anchoredPosition.y);

                // Get the height of the scroll content
                float contentHeight = content.rect.height;

                // Calculate the vertical position of the scroll
                float verticalPosition = 0 + (selectedChildY / contentHeight);
                // Set the vertical position of the scroll

                scrollRect.verticalNormalizedPosition = 1 - verticalPosition;

            }
            else
            {
                float selectedChildX = selectedChild.anchoredPosition.x;

                // Get the width of the scroll content
                float contentWidth = content.rect.width;

                // Calculate the horizontal position of the scroll
                float horizontalPosition = 0 + (selectedChildX / contentWidth);

              
                // Set the horizontal position of the scroll

                scrollRect.horizontalNormalizedPosition = horizontalPosition;

            }


           

        }




    }

}
