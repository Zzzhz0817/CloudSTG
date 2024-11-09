using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField] GameObject explodeEffect;
    private Rigidbody2D rigidbody2d;
    private int explodeLevel;
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
        explodeLevel = fireLv;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            var oriantationVector = transform.rotation * Vector3.up;
            GameObject explode = Instantiate(explodeEffect, transform.position + oriantationVector*0.7f, Quaternion.Euler(0f, 0f, Random.Range(0f, 360.0f)));
            explode.transform.localScale *= (1 + 0.7f * explodeLevel);
            Destroy(gameObject);
        }

        RushEnemy rushEnemy = collider.GetComponent<RushEnemy>();
        if(rushEnemy != null)
        {
            var oriantationVector = transform.rotation * Vector3.up;
            GameObject explode = Instantiate(explodeEffect, transform.position + oriantationVector*0.7f, Quaternion.Euler(0f, 0f, Random.Range(0f, 360.0f)));
            explode.transform.localScale *= (1 + 0.7f * explodeLevel);
            Destroy(gameObject);
        }
    }
}