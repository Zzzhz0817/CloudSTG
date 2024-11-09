using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction = new Vector2(0, 1);
    private float speed = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = direction * speed * Time.deltaTime;
        transform.position = new Vector3(velocity[0], velocity[1], 0);


        if(transform.position.magnitude > 100)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;

    }
}
