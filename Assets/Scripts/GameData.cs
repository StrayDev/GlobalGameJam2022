using UnityEngine;
using UnityEngine.InputSystem;

public static class GameData
{
    // controls 
    public static InputDevice Player1Device = null;
    public static InputDevice Player2Device = null;

    //Lane Info
    public static int Lane_Count = 5;
    public static float Lane_Width = 2f;
    public static float Lane_Start_X = -(Lane_Count-1); //Calculates the Intial position from Bottom Left
                                                                     //e.g. -(5-1) = -(4) = X: -4
    
}
