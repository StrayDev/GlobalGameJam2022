using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SwapPositions : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Transform playerOne = default;
    [SerializeField] private Transform playerTwo = default;

    [Header("Positions")] 
    [SerializeField] private float forwardPosition = 0;
    [SerializeField] private float backPosition = -5;

    [Header("Vars")] 
    [SerializeField] private float transitionTime = 1;

    [Header("Events")]
    [SerializeField] private UnityEvent _swapCompletedEvent = default;

    private Coroutine _swapPositionCoroutine = null;

    private Transform frontPlayer = default;
    private Transform backPlayer = default;
    
    void Start()
    {
        // set player one in front and player two behind
        playerOne.transform.position += new Vector3(0,0, forwardPosition);
        frontPlayer = playerOne;
        playerTwo.transform.position += new Vector3(0,0, backPosition);
        backPlayer = playerTwo;
    }

    public void SwapPosition()
    {
        // return if async running
        if (_swapPositionCoroutine != null) return;
        
        // set and run task
        var b = StartCoroutine(SwapPositionAsync(playerOne, playerTwo));
    }

    private IEnumerator SwapPositionAsync(Transform p1, Transform p2)
    {
        var centerPoint = (frontPlayer.position + backPlayer.position) / 2;

        var timer = transitionTime;
        while (timer > 0)
        {
            p1.RotateAround(centerPoint, Vector3.up, 180 / transitionTime * Time.deltaTime);
            p1.transform.forward = Vector3.forward;
            
            p2.RotateAround(centerPoint, Vector3.up, 180 / transitionTime * Time.deltaTime);
            p2.transform.forward = Vector3.forward;


            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        // reset task
        _swapPositionCoroutine = null;
        
        // swap completed
        _swapCompletedEvent.Invoke();
        
        // end coroutine
        yield return null;
    }
}
