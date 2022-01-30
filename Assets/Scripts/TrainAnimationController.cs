using UnityEngine;

public class TrainAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start playing base layer entry state automatically and second additive layer rear wheel state manually
        var anim = GetComponent<Animator>();
        anim.Play("Second Layer.RearWheel",1);
    }
}
