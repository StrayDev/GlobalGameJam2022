using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class tutorialScript : MonoBehaviour
{
    [SerializeField] private float start_delay = 1f;
    [SerializeField] private float time_between_texts = 2f;
    [SerializeField] List<string> turorial_text;

    [SerializeField] TMP_Text text;

    private IEnumerator doTutorial() {
        yield return new WaitForSeconds(start_delay);
        foreach (var element in turorial_text)
        {
            text.text = element;
            yield return new WaitForSeconds(time_between_texts);
        }
        text.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(doTutorial());
    }

}
