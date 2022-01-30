using UnityEngine;
using UnityEngine.InputSystem;

public static class GameData
{
    // controls 
    public static InputDevice Player1Device = null;
    public static InputDevice Player2Device = null;

    // core
    public static int p1_score = 200;
    public static int p2_score = 300;

    // Lane Info
    public static int Lane_Count = 5;
    public static float Lane_Width = 2.5f;
    public static float Lane_Start_X = -(Lane_Count); //Calculates the Intial position from Bottom Left
                                                                     //e.g. -(5-1) = -(4) = X: -4

    // Functions
    public static float getLaneXPos(float _lane) => Lane_Start_X + Lane_Width*_lane;
    public static void resetAllScore() { p1_score = 0; p2_score = 0; }
}
