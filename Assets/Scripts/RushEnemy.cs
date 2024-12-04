using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : MonoBehaviour
{
    public Transform wayPointCollection;
    [SerializeField] GameObject drop;
    [SerializeField] GameObject dieEffect;
    [SerializeField] bool isFacingRight;
    public int health = 3;
    public float moveSpeed = 8f; // Maximum move speed
    public float restTime = 5f;
    private float restTimer = 65535f;
    private bool timerFlag;
    private List<Vector3> wayPointPos;
    private int wayPointCount = 0;
    private Rigidbody2D rigidbody2d;
    private GameObject player;

    private float moveProgress = 0f; // Progress along the movement (0 to 1)
    private Vector3 startPoint;
    private Vector3 targetPoint;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        rigidbody2d = GetComponent<Rigidbody2D>();
        wayPointPos = new List<Vector3>();
        for (int i = 0; i < wayPointCollection.childCount; i++)
        {
            wayPointPos.Add(wayPointCollection.GetChild(i).transform.position);
        }

        // Initialize starting and target points
        startPoint = transform.position;
        targetPoint = wayPointPos[wayPointCount];
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        restTimer += Time.deltaTime;

        // Only move if the rest timer allows it
        if (restTimer > restTime)
        {
            // Increment move progress
            moveProgress += Time.deltaTime * moveSpeed / Vector3.Distance(startPoint, targetPoint);

            // Use a smooth easing function for natural movement
            float easedProgress = Mathf.SmoothStep(0f, 1f, moveProgress);

            // Update position
            transform.position = Vector3.Lerp(startPoint, targetPoint, easedProgress);

            // Check if the target point is reached
            if (moveProgress >= 1f)
            {
                // Move to the next waypoint
                wayPointCount++;
                wayPointCount %= wayPointPos.Count;

                // Reset for the next movement
                startPoint = targetPoint;
                targetPoint = wayPointPos[wayPointCount];
                moveProgress = 0f;

                // Handle rest timer and facing direction logic
                if (wayPointCount == 1 && timerFlag)
                {
                    restTimer = 0f;
                    timerFlag = false;
                }
                else if (wayPointCount == 2)
                {
                    if (isFacingRight)
                    {
                        transform.localScale = new Vector3(-0.03f, 0.03f, 0.06f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(0.03f, 0.03f, 0.06f);
                    }
                }
                else if (wayPointCount == 0)
                {
                    if (isFacingRight)
                    {
                        transform.localScale = new Vector3(0.03f, 0.03f, 0.06f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-0.03f, 0.03f, 0.06f);
                    }
                    timerFlag = true;
                }
            }
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
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
