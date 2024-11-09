using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public GameObject projectilePrefab;
    public Transform wayPointCollection;
    public int health = 3;

    public float shotInterval;
    public int shotSpeed;
    public float moveSpeed = 1f;
    private float shotTimer = 0f;
    private List<Vector3> wayPointPos;
    private int wayPointCount = 0;
    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        for (int i = 0; i < wayPointCollection.childCount; i++)
        {
            wayPointPos = new List<Vector3>();
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
            
            if (transform.position == wayPointPos[wayPointCount])
            {
                wayPointCount ++;
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
        Vector2 targetPosition = new Vector2(target.transform.position[0], target.transform.position[1]);
        Vector2 launchDirection = targetPosition - new Vector2(transform.position[0], transform.position[1]);
        launchDirection.Normalize();

        LaunchAProjectile(launchDirection, shotSpeed);
        LaunchAProjectile(Quaternion.AngleAxis(20f, Vector3.forward) * launchDirection, shotSpeed);
        LaunchAProjectile(Quaternion.AngleAxis(-20f, Vector3.forward) * launchDirection, shotSpeed);
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
        Destroy(gameObject);
    }
}
