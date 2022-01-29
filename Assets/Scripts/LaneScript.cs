using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    [SerializeField] private int currentLane = 3;
    public int CurrentLane => currentLane;
    [SerializeField] private float MOVE_DELAY = 0.3f;
    [SerializeField] private InputHandler _inputHandler;

    private bool moving = false;
    private float destination;

    private void Start()
    {
        _inputHandler = this.GetComponent<InputHandler>();
        Vector3 pos = _inputHandler.gameObject.transform.position;
        _inputHandler.transform.position = new Vector3(GameData.Lane_Start_X + GameData.Lane_Width * currentLane, pos.y,pos.z);
        moving = false;
    }
    void Update()
    {
        if (moving)
        {
            Vector3 pos = _inputHandler.gameObject.transform.position;
            this.transform.position = new Vector3(Mathf.Lerp(pos.x, destination, MOVE_DELAY), pos.y, pos.z);
        }
    }
    public void SetLane(float value)
    {
        if (!_inputHandler.Moved() && value > 0.1f)
        {
            if (currentLane < GameData.Lane_Count-1)
            {
                currentLane++;
            }
            _inputHandler.setMoved(true);
            StartCoroutine(movePlayer());
        }
        if (!_inputHandler.Moved() && value < -0.1f)
        {
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
        destination = GameData.Lane_Start_X + GameData.Lane_Width * currentLane;
        moving = true;
        yield return new WaitForSeconds(MOVE_DELAY);
        moving = false;
        _inputHandler.setMoved(false);
    }

    public void SetLane(int new_lane)
    {
        currentLane = new_lane;
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(GameData.Lane_Start_X + GameData.Lane_Width * currentLane, pos.y, pos.z);
    }
}


