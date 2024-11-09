using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    private Rigidbody2D rigidbody2d;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            var oriantationVector = transform.rotation * Vector3.up;
            Debug.Log(oriantationVector);
            Debug.Log(transform.position);
            Instantiate(hitEffect, transform.position + oriantationVector, transform.rotation);
            enemy.GetHit(damage);
            Destroy(gameObject);
        }
    }
}
