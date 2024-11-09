using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : MonoBehaviour
{
    public Transform wayPointCollection;
    public GameObject drop;
    public int health = 3;
    public float moveSpeed = 4f;
    private List<Vector3> wayPointPos;
    private int wayPointCount = 0;
    private Rigidbody2D rigidbody2d;
    private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        rigidbody2d = GetComponent<Rigidbody2D>();
        wayPointPos = new List<Vector3>();
        for (int i = 0; i < wayPointCollection.childCount; i++)
        {
            wayPointPos.Add(wayPointCollection.GetChild(i).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }


        transform.position = Vector3.MoveTowards(transform.position, wayPointPos[wayPointCount], moveSpeed * Time.deltaTime);

        if (transform.position == wayPointPos[wayPointCount])
        {
            wayPointCount ++;
            wayPointCount = wayPointCount % wayPointPos.Count;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player hitPlayer = collider.GetComponent<Player>();
        if (hitPlayer != null)
        {
            hitPlayer.GetHit();
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        Instantiate(drop, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}