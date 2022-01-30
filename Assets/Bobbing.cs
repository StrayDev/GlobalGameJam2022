using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    [SerializeField] private float bobMulti = 5.0f;

    private float t = 0;
    private bool goingUp = true;

    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float increase = (goingUp ? 1 : -1);
        float delta = (increase - t) * Time.deltaTime * bobMulti;
        if (goingUp) 
            delta = Mathf.Max(delta, 0.0001f);
        else 
            delta = Mathf.Min(delta, -0.0001f);
        t = t + delta;
        transform.localPosition = Vector3.Lerp(startPos, startPos + (transform.up * increase), t);
        if(goingUp && t >= 1.0f)
        {
            t = 1.0f;
            goingUp = false;
        }
        else if(t <= -1.0f)
        {
            t = -1.0f;
            goingUp = true;
        }
        Debug.Log(t);
    }
}
