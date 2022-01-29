using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrain : MonoBehaviour
{
    [SerializeField]
    private Transform ghostAttachPoint;

    [SerializeField]
    private float ghostSpacing;

    [SerializeField]
    private bool isPhysical;

    [SerializeField] UnityEvent pickUpGhost = default; 

    private List<Ghost> ghostsAttached;

    private int numGhosts = 0;

    InputActions input;

    // Start is called before the first frame update
    void Start()
    {
        ghostsAttached = new List<Ghost>();
        input = new InputActions();
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // REMOVE THIS LATER
        /*if(input.Player.Select.WasPressedThisFrame())
        {
            RemoveGhosts(2);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPhysical == false) return;
        Ghost ghostParent = collision.collider.GetComponentInParent<Ghost>();
        if (ghostParent && ghostsAttached.Contains(ghostParent) == false)
        {
            ghostParent.transform.SetParent(transform);
            ghostParent.transform.position = ghostAttachPoint.position - (transform.forward * ghostSpacing * Mathf.Ceil(numGhosts/2) + (transform.right * (numGhosts % 2)));
            ghostParent.GetComponentInChildren<Collider>().enabled = false;
            numGhosts++;
            pickUpGhost?.Invoke();
            ghostsAttached.Add(ghostParent);
        }
    }

    public void RemoveGhosts(int numberToRemove)
    {
        List<Ghost> ghostsToRemove = new List<Ghost>();
        for(int i = ghostsAttached.Count-1; i > ghostsAttached.Count-1 - numberToRemove; --i)
        {
            if (i < 0) break;
            Ghost toRemove = ghostsAttached[i];
            ghostsToRemove.Add(toRemove);
        }
        foreach(Ghost ghost in ghostsToRemove)
        {
            ghostsAttached.Remove(ghost);
            numGhosts--;
            Destroy(ghost.gameObject);
        }
    }
}
