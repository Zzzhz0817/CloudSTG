using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform wayPointCollection;
    [SerializeField] GameObject drop;
    [SerializeField] GameObject dieEffect;
    public int health = 3;

    public float shotInterval;
    public int shotNum;
    public int shotSpeed;
    public float moveSpeed = 1f;
    private float shotTimer = 0f;
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

        if (wayPointCount < wayPointPos.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPointPos[wayPointCount], moveSpeed * Time.deltaTime);
            if (Vector3.MoveTowards(transform.position, wayPointPos[wayPointCount], 1f)[0] < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale[0]),transform.localScale[1],transform.localScale[2]);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale[0]),transform.localScale[1],transform.localScale[2]);
            }

            if (transform.position == wayPointPos[wayPointCount])
            {
                wayPointCount ++;
            }
        }

        if (wayPointCount >= 5)
        {
            if (wayPointCount >= wayPointPos.Count)
            {
                Destroy(gameObject);
            }
        }
        

        

        shotTimer += Time.deltaTime;

        if(shotTimer >= shotInterval)
        {
            Launch();
            shotTimer = 0f;
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

    private void Launch()
    {
        Vector2 playerPosition = new Vector2(player.transform.position[0], player.transform.position[1]);
        Vector2 launchDirection = playerPosition - new Vector2(transform.position[0], transform.position[1]);
        launchDirection.Normalize();

        if (shotNum == 1)
        {
            LaunchAProjectile(launchDirection, shotSpeed);
        }
        else if (shotNum == 2)
        {
            LaunchAProjectile(Quaternion.AngleAxis(15f, Vector3.forward) * launchDirection, shotSpeed);
            LaunchAProjectile(Quaternion.AngleAxis(-15f, Vector3.forward) * launchDirection, shotSpeed);
        }
        else if (shotNum == 3)
        {
            LaunchAProjectile(launchDirection, shotSpeed);
            LaunchAProjectile(Quaternion.AngleAxis(20f, Vector3.forward) * launchDirection, shotSpeed);
            LaunchAProjectile(Quaternion.AngleAxis(-20f, Vector3.forward) * launchDirection, shotSpeed);
        }
        
    }

    private void LaunchAProjectile(Vector2 launchDirection, float shotSpeed)
    {

        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        EnemyBullet bullet = projectileObject.GetComponent<EnemyBullet>();
        bullet.Launch(launchDirection, shotSpeed);
    }

    public void GetHit(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
