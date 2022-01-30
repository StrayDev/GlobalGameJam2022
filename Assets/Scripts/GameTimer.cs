using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameTimer : MonoBehaviour
{
    //Time Data
    public float roundTime { get; private set; } = 120.0f;
    public float currentTime { get; private set; }
    [SerializeField] private LoadEndScene loadEndScene;
    [SerializeField] private TMP_Text text;

    [SerializeField] private UnityEvent gameOver = default;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        currentTime = roundTime;
        while (currentTime > 0)
        {
            yield return new WaitForEndOfFrame();
            currentTime -= Time.deltaTime;
            text.text = ((int)currentTime).ToString();
        }
        gameOver?.Invoke();
        loadEndScene.LoadTimesUp();
        //Game Over here, likely using another script.
    }
}
