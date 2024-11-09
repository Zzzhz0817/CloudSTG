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
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(2);
        Player hitPlayer = collision.gameObject.GetComponent<Player>();
        if (hitPlayer != null)
        {
            hitPlayer.GetHit();
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;
        Debug.Log(3);
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log(3);
    }
}
