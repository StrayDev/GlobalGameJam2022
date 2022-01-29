using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerTrain : MonoBehaviour
{
    private int numGhosts = 0;

    [SerializeField]
    private Transform ghostAttachPoint;

    [SerializeField]
    private float ghostSpacing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ghost ghostParent = collision.collider.GetComponentInParent<Ghost>();
        if (ghostParent)
        {
            ghostParent.transform.SetParent(transform);
            ghostParent.transform.position = ghostAttachPoint.position - (transform.forward * ghostSpacing * numGhosts);
            ghostParent.GetComponentInChildren<Collider>().enabled = false;
            numGhosts++;
        }
    }
}
