
using UnityEngine.SceneManagement;

public class GameKeys
    
{
    public const string NotEnoughMoney = "Not enough Money";

    //Car 
    public const string PathLocation_Wheel = "Wheels/Wheel_";
    public const string PathLocation_AllWheel = "Wheels/";


    //Tags
    public const string Tag_Player = "Player";
    public const string Tag_Camera = "MainCamera";

    //GamePlayer
    public const string RaceMode = "RaceMode";

    //IAPs
    public const string ItsBuyedNos = "ItsBuyedNos";

    //Values
    public static string SceneToLoad = "";
    //Scenes
    public static string EmptyScene = "EmptyScene";
    public static string Scene_Menu = "Menu";//scene index 2 
    public static string Scene_Test = "Test";//scene index 3 
    public static string Scene_Tutorial = "Tutorial";//scene index 4 


    //Save
    public const string CurrentMission = "CurrentMission";
    public const string FirstEnter = "FirstEnter";
    public const string NewGame = "NewGame";
    public const string PlayerStats = "PlayerStats";
    public const string PlayerLevel = "PlayerLevel";
    public const string PrideStats = "PrideStats";
    public const string PrideLions = "PrideLions";
    public const string CurrentScene = "CurrentScene";
    public const string itsTransitSectors = "itsTransitSectors";
    public const string PreviousSectors = "PreviousSectors";
    public const string ItsWithCompanion = "ItsWithCompanion";
    public const string PrideIsUnlock= "PrideIsUnlock";
    public const string MeatInfo = "MeatInfo";
    public const string MissionProgress = "MissionProgress";



    //Inputs
    public const string N_AButton = "A";
    public const string N_BButton = "B";
    public const string N_XButton = "X";
    public const string N_YButton = "Y";

    public const string N_RightButton = "Right";
    public const string N_LeftButton = "Left";
    public const string N_UpButton = "Up";
    public const string N_DownButton = "Down";

    public const string N_RButton = "R";
    public const string N_LButton = "L";

    public const string N_LeftStick = "LeftStick";
    public const string N_LeftStickX = "LeftStickX";
    public const string N_LeftStickY = "LeftStickY";

    public const string N_LeftStick_Left = "LeftStick_Left";
    public const string N_LeftStick_Right = "LeftStick_Right";
    public const string N_LeftStick_Up = "LeftStick_Up";
    public const string N_LeftStick_Down = "LeftStick_Down";


    public const string N_RightStick = "RightStick";
    public const string N_RightStickX = "RightStickX";
    public const string N_RightStickY = "RightStickY";

    public const string N_RightStick_Left = "RightStick_Left";
    public const string N_RightStick_Right = "RightStick_Right";
    public const string N_RightStick_Up = "RightStick_Up";
    public const string N_RightStick_Down = "RightStick_Down";

    public const string N_ZLButton = "ZL";
    public const string N_ZRButton = "ZR";

    public const string N_PlusButton = "Plus";
    public const string N_MinusButton = "Minus";

    public static void LoadEmptyScene(string sceneName)
    {
        NintendoCanvas.Instance.loadingScreen.SetActive(true);
        SceneToLoad = sceneName;
        SceneManager.LoadScene(GameKeys.EmptyScene);
    }
}
