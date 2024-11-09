using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWater : MonoBehaviour
{
    [SerializeField] GameObject penetrateEffect;
    private Rigidbody2D rigidbody2d;
    private int penetrateCount;
    public int damage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 20)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction*force);
    }

    public void SetLevel(int fireLv, int waterLv)
    {
        penetrateCount = waterLv;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            var oriantationVector = transform.rotation * Vector3.up;
            enemy.GetHit(damage);
            if (penetrateCount <= 0)
            {
                GameObject effect = Instantiate(penetrateEffect, transform.position + oriantationVector*0.7f, transform.rotation);
                effect.transform.localScale *= 2;
                Destroy(gameObject);
            }
            else
            {
                Instantiate(penetrateEffect, transform.position + oriantationVector*0.7f, transform.rotation);
                penetrateCount --;
            }
            
        }

        RushEnemy rushEnemy = collider.GetComponent<RushEnemy>();
        if(rushEnemy != null)
        {
            var oriantationVector = transform.rotation * Vector3.up;
            rushEnemy.GetHit(damage);
            if (penetrateCount <= 0)
            {
                GameObject effect = Instantiate(penetrateEffect, transform.position + oriantationVector*0.7f, transform.rotation);
                effect.transform.localScale *= 2;
                Destroy(gameObject);
            }
            else
            {
                Instantiate(penetrateEffect, transform.position + oriantationVector*0.7f, transform.rotation);
                penetrateCount --;
            }
        }
    }
}

