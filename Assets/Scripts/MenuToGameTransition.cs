using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MenuToGameTransition : MonoBehaviour
{
    public StudioEventEmitter confirmNoise;
    public void OnReadyButton()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        confirmNoise.Play();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainScene");
    }
}
