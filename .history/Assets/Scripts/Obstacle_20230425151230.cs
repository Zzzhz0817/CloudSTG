using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.magnitude > 40)
        {
            Destroy(gameObject);
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

