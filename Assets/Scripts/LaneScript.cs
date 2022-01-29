using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    [SerializeField] private int currentLane = 3;
    [SerializeField] private const int MAX_LANE = 4;
    [SerializeField] private const float X_DISTANCE = 2f;
    [SerializeField] private const float X_START = -4f;
    [SerializeField] private InputHandler _inputHandler;

    private bool moving = false;
    private float destination;

    private void Start()
    {
        _inputHandler = this.GetComponent<InputHandler>();
        Vector3 pos = _inputHandler.gameObject.transform.position;
        _inputHandler.transform.position = new Vector3(X_START + X_DISTANCE * currentLane, pos.y,pos.z);
        moving = false;
    }
    void Update()
    {
        /*
        if (moving)
        {
            Vector3 pos = _inputHandler.gameObject.transform.position;
            
        }
        */
    }
    public void SetLane(float value)
    {
        if (!_inputHandler.Moved() && value > 0.1f)
        {
            Debug.Log("Move Right Activated");
            if (currentLane < MAX_LANE)
            {
                currentLane++;
            }
            _inputHandler.setMoved(true);
            StartCoroutine(movePlayer());
        }
        if (!_inputHandler.Moved() && value < -0.1f)
        {
            Debug.Log("Move left Activated");
            if (currentLane > 0)
            {
                currentLane--;
            }
            _inputHandler.setMoved(true);
            StartCoroutine(movePlayer());
        }
    }

    IEnumerator movePlayer()
    {
        //destination = X_START + X_DISTANCE * currentLane;
        moving = true;
        yield return new WaitForSeconds(0.5f);
        moving = false;
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(X_START + X_DISTANCE * currentLane, pos.y, pos.z);
        Debug.Log("Finished Moving Player");
        _inputHandler.setMoved(false);
    }
}
