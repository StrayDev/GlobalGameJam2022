using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text p1_score;
    [SerializeField] private TMP_Text p2_score;

    private void Start()
    {
        p1_score.text = GameData.p1_score.ToString();
        p2_score.text = GameData.p2_score.ToString();
    }
}
