using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0.0f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if(enemyController != null)
        {
            enemyController.Fix();
        }
        Destroy(gameObject);
    }
}