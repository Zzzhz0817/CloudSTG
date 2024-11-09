using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Vector2 lookDirection = new Vector2(1, 0);
    private Rigidbody2D rigidbody2d;

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
        if (Input.touchCount == 1)
        {
            Vector2 world_pos = ScreenToWorld(Input.GetTouch(0).position);
            transform.position = world_pos;
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (Input.touchCount == 2)
        {   
            Vector2 world_pos0, world_pos1;
            if (Input.GetTouch(0).position[0] < Input.GetTouch(1).position[0])
            {
                world_pos0 = ScreenToWorld(Input.GetTouch(0).position);
                world_pos1 = ScreenToWorld(Input.GetTouch(1).position);
            }
            else
            {
                world_pos1 = ScreenToWorld(Input.GetTouch(0).position);
                world_pos0 = ScreenToWorld(Input.GetTouch(1).position);
            }

            Vector2 world_pos = (world_pos0 + world_pos1) / 2;
            
            float rotation_angle = Mathf.Atan2(world_pos0[0]-world_pos1[0], world_pos0[1]-world_pos1[1]);
            rotation_angle = -180 * rotation_angle / Mathf.PI;
            lookDirection = new Vector2(1, Mathf.Tan(rotation_angle));
            lookDirection.Normalize();
            Debug.Log(rotation_angle);
            lookDirection = new Vector2(1, 0);


            transform.position = world_pos;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_angle);
        }
        shotTimer += Time.deltaTime;

        if(shotTimer >= shotInterval)
        {
            Launch();
            shotTimer = 0f;
        }
    }

    private Vector2 ScreenToWorld(Vector2 screen_pos)
    {   
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;

        float y = 2 * halfHeight * screen_pos[1] / (float) Screen.height - halfHeight;
        float x = 2 * halfWidth * screen_pos[0] / (float) Screen.width - halfWidth;
        Vector2 world_pos = new Vector2(x, y);
        return world_pos;
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, shotSpeed);
    }
}
