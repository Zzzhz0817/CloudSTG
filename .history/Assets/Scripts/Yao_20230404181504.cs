using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yao : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public bool isYang;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 10)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.GetYao();
            Destroy(gameObject);
        }
    }
}
