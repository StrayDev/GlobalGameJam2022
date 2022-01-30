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

    private FMOD.Studio.EventInstance trainEvent;
    [SerializeField] private float progress = 0;    
    [SerializeField] [FMODUnity.EventRef] private string trainPath; 

    // Start is called before the first frame update0
    void Start()
    {
        trainEvent = FMODUnity.RuntimeManager.CreateInstance(trainPath);
        //trainEvent.setParameterByName("Progress", progress);
        StartCoroutine(CountDown());
        trainEvent.start();
        trainEvent.release();
    }

    public IEnumerator CountDown()
    {
        currentTime = roundTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            text.text = ((int)currentTime).ToString();
            progress = (currentTime/roundTime)*10;
            yield return new WaitForEndOfFrame();
        }
        gameOver?.Invoke();
        loadEndScene.LoadTimesUp();
        //Game Over here, likely using another script.
    }

    void update() {
        trainEvent.setParameterByName("Progress",progress);
    }
}
