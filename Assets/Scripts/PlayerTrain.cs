using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

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

    public StudioEventEmitter hornNoise;

    private int numGhosts = 0;

    InputActions input;

    // Start is called before the first frame update
    void Start()
    {
        ghostsAttached = new List<Ghost>();
        input = new InputActions();
        input.Player.Enable();
        SetPhysical(isPhysical);
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
            ghostParent.transform.position = ghostAttachPoint.position - (transform.forward * ghostSpacing * Mathf.Ceil(numGhosts/3) + (transform.right * (numGhosts % 3)));
            ghostParent.GetComponentInChildren<Collider>().enabled = false;
            numGhosts++;
            pickUpGhost?.Invoke();
            ghostsAttached.Add(ghostParent);
            ghostParent.disableParticles();
            StudioEventEmitter emitter = ghostParent.GetComponent<StudioEventEmitter>();
            emitter.Play();
            GameData.increaseScore(gameObject.GetComponent<InputHandler>().getPlayerID() == Player.PlayerOne,10);
        }
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            RemoveGhosts(3);
            StudioEventEmitter emitter = collision.gameObject.GetComponent<StudioEventEmitter>();
            emitter.Play();
            StartCoroutine(collision.gameObject.GetComponent<Obstacle>().killSelf());
            Debug.Log("Obstacle hit");
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
            GameData.increaseScore(gameObject.GetComponent<InputHandler>().getPlayerID() == Player.PlayerOne,-10);
            ghost.GetComponent<Rigidbody>().useGravity = true;
            ghost.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void SetPhysical(bool physical)
    {
        isPhysical = physical;
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach(Material material in renderer.materials)
            {
                Color color = material.color;
                color.a = isPhysical ? 1.0f : 0.5f;
                material.color = color;
            }
        }
    }
}
