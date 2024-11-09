using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Vector2 orientationVector = new Vector2(1, 0);
    private float orientationAngle = 0f;
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
            orientationVector = new Vector2(0, 1);
            orientationAngle = 0;
            transform.rotation = Quaternion.Euler(0f, 0f, orientationAngle + 90);
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
            
            orientationAngle = Mathf.Atan2(world_pos0[0]-world_pos1[0], world_pos0[1]-world_pos1[1]) - 90;
            orientationAngle = -180 * orientationAngle / Mathf.PI;

            orientationVector = new Vector2(Mathf.Tan(Mathf.PI * (orientationAngle) / -180), 1);
            orientationVector.Normalize();

            transform.position = world_pos;
            transform.rotation = Quaternion.Euler(0f, 0f, orientationAngle + 90);
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
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, orientationAngle));
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(orientationVector, shotSpeed);
    }
}
