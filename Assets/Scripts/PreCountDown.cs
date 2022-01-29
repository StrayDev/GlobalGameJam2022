using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreCountDown : MonoBehaviour
{
    [SerializeField] private int preCountDown = 5;
    [SerializeField] private GameObject gameTimer;

    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrecountDown(preCountDown));
    }

    IEnumerator PrecountDown(float _duration)
    {
        //yield return new WaitForSeconds(_duration);
        float e_time = 0;
        while(e_time < _duration)
        {
            e_time += Time.deltaTime;
            //
            text.text = (_duration - e_time).ToString();
            //

            yield return null;
        }
        gameTimer.SetActive(true);
        text.gameObject.SetActive(false);
    }
}
