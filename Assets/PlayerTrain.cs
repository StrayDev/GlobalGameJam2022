using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerTrain : MonoBehaviour
{
    [SerializeField]
    private Transform ghostAttachPoint;

    [SerializeField]
    private float ghostSpacing;

    [SerializeField]
    private bool isPhysical;

    private int numGhosts = 0;

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
        if (isPhysical == false) return;
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
