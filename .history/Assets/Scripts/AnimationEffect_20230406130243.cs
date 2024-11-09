using UnityEngine;
using System.Collections;
 
public class AnimationEffect: MonoBehaviour {
    public float delay = 0f;
    public int damage = 0;
    public float scale = 0;


    void Start ()
    {
        Debug.Log(gameObject);
        if (scale != 0)
        {
            transform.localScale = new Vector3(scale,scale,scale);
        }
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (damage > 0)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.GetHit(damage);
            }

            RushEnemy rushEnemy = collider.GetComponent<RushEnemy>();
            if(rushEnemy != null)
            {
                rushEnemy.GetHit(damage);
            }
        }

    }



}