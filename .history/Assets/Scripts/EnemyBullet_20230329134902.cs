using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
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
        float orientationAngle = Mathf.Atan2(direction[1], direction[0]);
        orientationAngle = -180 * orientationAngle / Mathf.PI;
        
        transform.rotation = Quaternion.Euler(0f, 0f, orientationAngle);
        rigidbody2d.AddForce(direction*force);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.GetHit();
        }
    }
}
