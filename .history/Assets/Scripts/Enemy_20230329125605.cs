using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public int health = 3;
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
            Debug.Log("player damaged!");
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;
        Debug.Log("hit");
    }
}
