using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player hitPlayer = collision.gameObject.GetComponent<Player>();
        if (hitPlayer != null)
        {
            Debug.Log("player hit!");
        }
    }
}
