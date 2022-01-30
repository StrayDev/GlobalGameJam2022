using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject renderObject;
    public IEnumerator killSelf() {
        transform.SetParent(null);
        renderObject.SetActive(false);
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    } 
}
