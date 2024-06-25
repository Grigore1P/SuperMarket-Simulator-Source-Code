using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum NavigateDirection
{
    Up,
    Down,
    Left,
    Right,
}
public class ChangeExplicitNavigation : MonoBehaviour
{
    [SerializeField] private Selectable ui_Need_ToChange_Selectable;

    public void ChnageSelectable(Selectable navigateTo, NavigateDirection navigateDirection)
    {
        var navigation = ui_Need_ToChange_Selectable.navigation;
        switch (navigateDirection)
        {
               
            case NavigateDirection.Up:
               navigation.selectOnUp = navigateTo;
                break;
            case NavigateDirection.Down:
                navigation.selectOnDown = navigateTo;
                break;
            case NavigateDirection.Left:
                navigation.selectOnLeft = navigateTo;
                break;
            case NavigateDirection.Right:
                navigation.selectOnRight = navigateTo;
                break;
        }

        ui_Need_ToChange_Selectable.navigation = navigation;



    }

}
