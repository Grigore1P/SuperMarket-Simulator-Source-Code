using Rewired;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class InputActions : MonoBehaviour
{
    public static UnityEvent YButtonEvent = new UnityEvent();
    public static UnityEvent YButtonEventUI = new UnityEvent();

    public static UnityEvent BButtonEvent = new UnityEvent();
    public static UnityEvent BButtonEventUI = new UnityEvent();

    public static UnityEvent AButtonEvent = new UnityEvent();
    public static UnityEvent AButtonEventUI = new UnityEvent();

    public static UnityEvent XButtonEvent = new UnityEvent();
    public static UnityEvent XButtonEventUI = new UnityEvent();

    public static UnityEvent PlusButtonEvent = new UnityEvent();
    public static UnityEvent PlusButtonEventUI = new UnityEvent();

    public static UnityEvent RightButtonEvent = new UnityEvent();
    public static UnityEvent RightButtonEventUI = new UnityEvent();

    public static UnityEvent LeftButtonEvent = new UnityEvent();
    public static UnityEvent LeftButtonEventUI = new UnityEvent();

    public static UnityEvent MinusButtonEvent = new UnityEvent();
    public static UnityEvent MinusButtonEventUI = new UnityEvent();

    public static UnityEvent RButtonEvent = new UnityEvent();
    public static UnityEvent RButtonEventUI = new UnityEvent();

    public static UnityEvent LButtonEvent = new UnityEvent();
    public static UnityEvent LButtonEventUI = new UnityEvent();

    public static UnityEvent<float> RightStickX = new UnityEvent<float>();
    public static UnityEvent<float> RightStickY = new UnityEvent<float>();

    public static UnityEvent<float> RightStickXUI = new UnityEvent<float>();
    public static UnityEvent<float> RightStickYUI = new UnityEvent<float>();

    public static UnityEvent<float> LeftStickX = new UnityEvent<float>();
    public static UnityEvent<float> LeftStickY = new UnityEvent<float>();

    public static UnityEvent ZLButtonEvent = new UnityEvent();
    public static UnityEvent ZLButtonEventUI = new UnityEvent();

    public static UnityEvent ZRButtonEvent = new UnityEvent();
    public static UnityEvent ZRButtonEventUI = new UnityEvent();

    public static UnityEvent<float> ZLButtonAxis = new UnityEvent<float>();
    public static UnityEvent<float> ZRButtonAxis = new UnityEvent<float>();

    public static UnityEvent UpButtonEvent = new UnityEvent();
    public static UnityEvent UpButtonEventUI = new UnityEvent();

    public static UnityEvent DownButtonEvent = new UnityEvent();
    public static UnityEvent DownButtonEventUI = new UnityEvent();

    public static UnityEvent AnyButtonEvent = new UnityEvent();


    public static bool isUiInGamePlay;
    public static bool isStopSendingEvents;

    [SerializeField] private float tempDelay = 0.1f;
    private Rewired.Player player;
    [SerializeField] private float actionsDelay;
  
    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
        Debug.Log(player.id + "Player ID");
       // RCC_InputManager.SubscribeToInputs();
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
       
        if (isStopSendingEvents)
        {
            ResetAxisValue();
            return;
        }
        if (player.GetAnyButton())
        {
            AnyButtonEvent.Invoke();
        }


        actionsDelay -= Time.unscaledDeltaTime;
        actionsDelay  = Mathf.Clamp(actionsDelay, -1, tempDelay);

       

        if (isUiInGamePlay)
        {
            InputForUI();
        }
        else
        {
            InputForGamePlay();
        }

     


    }

    private void InputForGamePlay()
    {

        //Axis
        RightStickX.Invoke(player.GetAxis(GameKeys.N_RightStickX));
        RightStickY.Invoke(player.GetAxis(GameKeys.N_RightStickY));

        LeftStickX.Invoke(player.GetAxis(GameKeys.N_LeftStickX));
        LeftStickY.Invoke(player.GetAxis(GameKeys.N_LeftStickY));

        ZLButtonAxis.Invoke(player.GetAxis(GameKeys.N_ZLButton));
        ZRButtonAxis.Invoke(player.GetAxis(GameKeys.N_ZRButton));

        if (actionsDelay > 0)
        {
            return;
        }

      
        if (player.GetButtonDown(GameKeys.N_YButton))
        {
            YButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_BButton))
        {
            BButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_PlusButton))
        {
            PlusButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_MinusButton))
        {
            MinusButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_AButton))
        {
            AButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_XButton))
        {
            XButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_RightButton))
        {
            RightButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_LeftButton))
        {
            LeftButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_RButton))
        {
            RButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_LButton))
        {
            LButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_ZLButton))
        {
            ZLButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_ZRButton))
        {
            ZRButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_UpButton))
        {
            UpButtonEvent.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_DownButton))
        {
            DownButtonEvent.Invoke();
            SetDelay();
        }

    }

    private void InputForUI()
    {
        //Axis
        RightStickXUI.Invoke(player.GetAxis(GameKeys.N_RightStickX));
        RightStickYUI.Invoke(player.GetAxis(GameKeys.N_RightStickY));

        ResetAxisValue();

        if(actionsDelay > 0)
        {
            return;
        }
        if (player.GetButtonDown(GameKeys.N_YButton))
        {
            YButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_BButton))
        {
            BButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_PlusButton))
        {
            PlusButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_MinusButton))
        {
            MinusButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_AButton))
        {
            AButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_XButton))
        {
            XButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_RightButton))
        {
            RightButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_LeftButton))
        {
            LeftButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_RButton))
        {
            RButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_LButton))
        {
            LButtonEventUI.Invoke();
            SetDelay();
        }

        if (player.GetButtonDown(GameKeys.N_ZLButton))
        {
            ZLButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_ZRButton))
        {
            ZRButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_UpButton))
        {
            UpButtonEventUI.Invoke();
            SetDelay();
        }
        if (player.GetButtonDown(GameKeys.N_DownButton))
        {
            DownButtonEventUI.Invoke();
            SetDelay();
        }


    }

    private void ResetAxisValue()
    {
        //Set to 0 value for axis on UI 
        RightStickX.Invoke(0);
        RightStickY.Invoke(0);

        LeftStickX.Invoke(0);
        LeftStickY.Invoke(0);

        ZLButtonAxis.Invoke(0);
        ZRButtonAxis.Invoke(0);



    }

    private void SetDelay()
    {
        actionsDelay = tempDelay;
        return;
    }
}
