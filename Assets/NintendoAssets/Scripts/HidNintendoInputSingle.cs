using UnityEngine;
using nn.hid;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using Rewired;

public class HidNintendoInputSingle : MonoBehaviour
{
    //private UnityEngine.UI.Text textComponent;
    //private System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

    public static NpadId[] npadIds = { NpadId.Handheld, NpadId.No1, NpadId.No2, NpadId.No3, NpadId.No4, NpadId.No5, NpadId.No6, NpadId.No7, NpadId.No8 };


    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadStyle npadStyleTemp = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();


    private ControllerSupportArg controllerSupportArg = new ControllerSupportArg();
    private nn.Result result = new nn.Result();

    private GameObject _CanvasWarning;
    private bool _isActivatePP = false;

    private bool _isSwap = false;
    private long[] preButtons;
    //private Rewired.InputManager rewired;

    public int playerId;
    private  Rewired.Player player;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void init()
    {
        Debug.LogError("init -> HidNintendoInputSingle");
        GameObject main = new GameObject("HidNintendoInputSingle");
        main.AddComponent<HidNintendoInputSingle>();
        GameObject.DontDestroyOnLoad(main);
    }

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        //rewired = FindObjectOfType<Rewired.InputManager>();
        //textComponent = GameObject.Find("/Canvas/Text").GetComponent<UnityEngine.UI.Text>();

        Npad.Initialize();
        NpadJoy.SetHoldType(NpadJoyHoldType.Horizontal);
        Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1, NpadId.No2, NpadId.No3, NpadId.No4, NpadId.No5, NpadId.No6, NpadId.No7, NpadId.No8 });
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
        preButtons = new long[1];

        //Debug.LogError($"Start Status -> {npadStyleTemp} -> {Npad.GetStyleSet(NpadId.No1)}");
        if (npadStyleTemp == NpadStyle.Invalid || Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
        {
            if (Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
            {
                npadStyle = Npad.GetStyleSet(NpadId.No1);
                npadId = NpadId.No1;
            }
            else
            {
                npadStyle = NpadStyle.Handheld;
                npadId = NpadId.Handheld;
            }

            npadStyleTemp = npadStyle;
            //Debug.LogError($"Start Status Set -> {npadStyleTemp} -> {npadStyle}");
        }

    }

    void Update()
    {
        //stringBuilder.Length = 0;

        NpadButton onButtons = 0;

        if (UpdatePadState())
        {
            NpadId npadIds = npadId;
            NpadStyle npadStyle = Npad.GetStyleSet(npadIds);
            //if (npadStyle == NpadStyle.None) { continue; }

            Npad.GetState(ref npadState, npadIds, npadStyle);

            onButtons |= ((NpadButton)preButtons[0] ^ npadState.buttons) & npadState.buttons;
            preButtons[0] = (long)npadState.buttons;

            //StartCoroutine(ShowPupupGame());


            NpadStateUP();
            //if (onButtons != 0)
            //{
            //    //ShowControllerSupport();
            //}


            StartCoroutine(ShowPupupGame());


            //if (npadState.GetButton(NpadButton.A))
            //{
            //    AnalogStickState lStick = npadState.analogStickL;
            //    stringBuilder.AppendFormat(
            //        "Press A Button\n<color=#ff8888ff>{0} {1} {2}\nanalogStickL({3}, {4})</color>",
            //        npadId, npadStyle, npadState, lStick.fx, lStick.fy);
            //}
            //else
            //{
            //    stringBuilder.AppendFormat(
            //        "Press A Button\n{0} {1} {2}", npadId, npadStyle, npadState);
            //}

        }
        else
        {
            for (int i = 0; i < npadIds.Length; i++)
            {
                NpadId npadId = npadIds[i];
                NpadStyle npadStyle = Npad.GetStyleSet(npadId);
                if (npadStyle == NpadStyle.None) { continue; }

                Npad.GetState(ref npadState, npadId, npadStyle);

                onButtons |= ((NpadButton)preButtons[0] ^ npadState.buttons) & npadState.buttons;
                preButtons[0] = (long)npadState.buttons;

                StartCoroutine(ShowPupupGame());

            }
        }

    }
    private void NpadStateUP()
    {
        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        Npad.GetState(ref no1State, NpadId.No1, no1Style);

        if ((npadStyleTemp != npadStyle && npadStyle == NpadStyle.Handheld && npadStyleTemp != NpadStyle.JoyDual && !_isSwap)
            || (npadStyle == NpadStyle.Handheld && npadStyleTemp != no1Style && no1Style == NpadStyle.FullKey && !_isSwap)
            || Npad.GetStyleSet(NpadId.No2) != NpadStyle.None && !_isSwap
            || npadStyle == NpadStyle.Handheld && no1Style == NpadStyle.JoyDual && !_isSwap)
        {
            //Debug.LogError($"Status == NpadStyle -> {npadStyle}\n npadStyleTemp -> {npadStyleTemp}\n no1Style -> {no1Style}");

            _isSwap = true;
            StopInput();
            ShowControllerSupport();
        }
        else
        {
            if (no1Style == NpadStyle.FullKey)
                npadStyle = NpadStyle.FullKey;
            else if (no1Style == NpadStyle.JoyDual)
                npadStyle = NpadStyle.JoyDual;
            else
                npadStyle = NpadStyle.Handheld;

            npadStyleTemp = npadStyle;

            if (npadStyle == NpadStyle.Handheld)
                Npad.Disconnect(NpadId.No1);

            _isSwap = false;
            StartInput();
        }

    }
    private bool UpdatePadState()
    {

        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None && npadStyleTemp != handheldStyle)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                npadId = NpadId.No1;
                npadStyle = no1Style;
                npadState = no1State;
                return true;
            }
        }

        if ((npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {
            npadId = NpadId.Handheld;
            //npadStyle = handheldStyle;
            npadState = handheldState;
        }
        else if ((npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {
            npadId = NpadId.No1;
            //npadStyle = no1Style;
            npadState = no1State;
        }
        else
        {
            npadId = NpadId.Invalid;
            npadStyle = NpadStyle.Invalid;
            npadState.Clear();
            return false;
        }
        return true;
    }


    void ShowControllerSupport()
    {
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
        controllerSupportArg.SetDefault();
        controllerSupportArg.playerCountMax = 1;
        controllerSupportArg.playerCountMin = 0; //must be set to 0 if you want to allow someone to play in handheld mode only
        controllerSupportArg.enablePermitJoyDual = true;
        //controllerSupportArg.enableTakeOverConnection = false;
        controllerSupportArg.enableSingleMode = true; // set to false to remove handheld image from applet

       // Debug.Log(controllerSupportArg);
        UnityEngine.Switch.Applet.Begin();
        result = ControllerSupport.Show(controllerSupportArg);
        UnityEngine.Switch.Applet.End();
        if (!result.IsSuccess()) { Debug.Log(result); }
    }

    IEnumerator ShowPupupGame()
    {
       // Debug.LogError(npadState.attributes + "       ");
        if (((npadState.attributes & NpadAttribute.IsLeftConnected) != 0) != ((npadState.attributes & NpadAttribute.IsRightConnected) != 0))
        {
            StopInput();
            if (!_isActivatePP)
            {
                _isActivatePP = true;
                //StopInput();

                yield return new WaitForSecondsRealtime(0.1f);
                //Create Info Panel
                /*_CanvasWarning = Instantiate(Resources.Load("Canvas", typeof(GameObject))) as GameObject;*/
                #if true
                                _CanvasWarning = new GameObject("Canvas");
                                Canvas c = _CanvasWarning.AddComponent<Canvas>();
                                c.renderMode = RenderMode.ScreenSpaceOverlay;
                                _CanvasWarning.AddComponent<CanvasScaler>();
                                _CanvasWarning.AddComponent<GraphicRaycaster>();
                                GameObject panel = new GameObject("Panel");
                                panel.AddComponent<CanvasRenderer>();
                                Text i = panel.AddComponent<Text>();
                                i.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                                i.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
                                i.text = "The Joy-Con grip accessory is recommended when playing.";
                                i.alignment = TextAnchor.MiddleCenter;
                                i.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
                                i.fontSize = 20;
                                panel.transform.SetParent(_CanvasWarning.transform, false);
                #endif
                

                if (((npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(0.1f);
                if (((npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(0.1f);
                if (((npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(0.1f);
                DestroyOBJ();
                if (((npadState.attributes & NpadAttribute.IsLeftConnected) != 0) != ((npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                {
                    Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
                    ShowControllerSupport();
                }
            }
        }
    }

    void DestroyOBJ()
    {
        _isActivatePP = false;
        StartInput();

        //Destroy Info Panel
        Destroy(_CanvasWarning);
    }

    void StartInput()
    {
        //Pornirea Inputului
        //rewired.enabled = true;
        player.controllers.maps.SetMapsEnabled(true, 0);
    }

    void StopInput()
    {
        //Oprirea Inputului
        player.controllers.maps.SetMapsEnabled(false, 0);
        //rewired.enabled = false;
    }
}
