using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToGameTransition : MonoBehaviour
{
    public void OnReadyButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
