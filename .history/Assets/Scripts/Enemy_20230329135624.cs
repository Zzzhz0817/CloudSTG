using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public GameObject projectilePrefab;
    public int health = 3;

    public float shotInterval;
    public int shotSpeed;
    private float shotTimer = 0f;
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

        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        EnemyBullet bullet = projectileObject.GetComponent<EnemyBullet>();
        bullet.Launch(new Vector2(0.5f, 0.5f), shotSpeed);
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
