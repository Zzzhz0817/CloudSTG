using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qi : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Transform playerTrans;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 10)
        {
            Destroy(gameObject);
        }

        if (transform.position - )
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.GetQi();
            Destroy(gameObject);
        }
    }
}
