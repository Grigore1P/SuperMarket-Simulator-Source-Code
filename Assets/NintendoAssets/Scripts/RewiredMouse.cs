using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewiredMouse : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public void OnScreenPositionChanged(Vector2 screenPosition)
    {
        if (Camera.main)
        {
            RectTransform canvasRectTransform = transform.root.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
            Rect rootCanvasRect = canvasRectTransform.rect;
            Vector2 viewportPos = Camera.main.ScreenToViewportPoint(screenPosition);
            viewportPos.x = (viewportPos.x * rootCanvasRect.width) - canvasRectTransform.pivot.x * rootCanvasRect.width;
            viewportPos.y = (viewportPos.y * rootCanvasRect.height) - canvasRectTransform.pivot.y * rootCanvasRect.height;
            (transform as RectTransform).anchoredPosition = viewportPos;
        }
       
    }
}
