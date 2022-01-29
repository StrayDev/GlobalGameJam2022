using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    //Time Data
    [SerializeField] private int CountDownTime = 120;
    [SerializeField] private LoadEndScene loadEndScene;
    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        int currentTime = CountDownTime;
        while (currentTime > 0)
        {   
            yield return new WaitForSeconds(1);
            currentTime--;
            text.text = currentTime.ToString();
        }
        loadEndScene.LoadTimesUp();
        //Game Over here, likely using another script.
    }
}
