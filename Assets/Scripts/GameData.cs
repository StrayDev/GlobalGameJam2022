using UnityEngine;
using UnityEngine.InputSystem;

public static class GameData
{
    // controls 
    public static InputDevice Player1Device = null;
    public static InputDevice Player2Device = null;

    // core
    public static int score = 0;

    // Lane Info
    public static int Lane_Count = 5;
    public static float Lane_Width = 2f;
    public static float Lane_Start_X = -(Lane_Count-1); //Calculates the Intial position from Bottom Left
                                                                     //e.g. -(5-1) = -(4) = X: -4

    // Functions
    public static float getLaneXPos(float _lane) => Lane_Start_X + Lane_Width*_lane;
    public static void resetScore() { score = 0; }
}
