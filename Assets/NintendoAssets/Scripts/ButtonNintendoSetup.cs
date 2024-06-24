using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNintendoSetup : MonoBehaviour
{
    public string actionName;

    private Button btn;

    private Rewired.Player rewiredPlayer;

    private void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    void Start()
    {
        if(GetComponent<Button>() != null)
          btn = GetComponent<Button>();
    }

    void Update()
    {
        if(btn == null) 
            return;
        
        if(rewiredPlayer.GetButtonDown(actionName) && btn.IsInteractable() && btn.gameObject.activeInHierarchy)
            btn.onClick.Invoke();
    }
}
