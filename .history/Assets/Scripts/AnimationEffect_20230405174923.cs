using UnityEngine;
using System.Collections;
 
public class AnimationEffect: MonoBehaviour {
    public float delay = 0f;
    public int damage = 0;
    private bool damagedFlag = false;


    void Start () {
        Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!damagedFlag)
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
            damagedFlag = true;
        }
    }



}