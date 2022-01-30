using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndScene : MonoBehaviour
{
    public void LoadTimesUp()
    {
        SceneManager.LoadScene(2);
    }
}
