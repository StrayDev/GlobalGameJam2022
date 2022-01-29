using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    [SerializeField] private int currentLane = 1;
    [SerializeField] private InputHandler _inputHandler;

    private void Start()
    {
        _inputHandler = this.GetComponent<InputHandler>();
    }
    void Update()
    {
        
    }
    public void SetLane(float value)
    {
        if (_inputHandler.Moved() && value > 0.1f || value < -0.1f)
        {
            Debug.Log("This Activated");
            _inputHandler.gameObject.transform.position += new Vector3(value, 0, 0);
            _inputHandler.setMoved(true);
            movePlayer();
        }
    }

    IEnumerator movePlayer()
    {
        
        yield return new WaitForSeconds(1);
        Debug.Log("Finished Moving Player");
        _inputHandler.setMoved(false);
    }
}
