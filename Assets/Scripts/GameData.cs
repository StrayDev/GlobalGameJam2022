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
    public static float Lane_Width = 10f;
    public static float Lane_Start_X = -(20); //Calculates the Intial position from Bottom Left
                                                                     //e.g. -(5-1) = -(4) = X: -4
    public static bool mid_swap {get; private set;} = false;

    public static bool isMidSwap() => mid_swap;
    public static void setMidSwap(bool _new_state) {mid_swap = _new_state;}

    // Functions
    public static float getLaneXPos(float _lane) => Lane_Start_X + Lane_Width*_lane;
    public static void resetAllScore() { p1_score = 0; p2_score = 0; }

    public static void increaseScore(bool _player_1, int _val) {
        if (_player_1) {
            p1_score += _val;
        } else {
            p2_score += _val;
        }
    }
}
