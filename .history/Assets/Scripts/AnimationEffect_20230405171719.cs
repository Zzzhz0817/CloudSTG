using UnityEngine;
using System.Collections;
 
public class AnimationEffect: MonoBehaviour {
    public float delay = 0f;
    public float damage = 0f;


    void Start () {
        Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
    }
    


}