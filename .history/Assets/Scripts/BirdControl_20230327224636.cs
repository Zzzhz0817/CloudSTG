using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    private Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Debug.Log(Input.GetTouch(0).position);
            Vector2 world_pos = ScreenToWorld(Input.GetTouch(0).position);
            Debug.Log(world_pos);
            transform.position = world_pos;
        }
        else if (Input.touchCount == 1)
        {
            Debug.Log(Input.GetTouch(0).position);
            Vector2 world_pos = ScreenToWorld(Input.GetTouch(0).position);
            Debug.Log(world_pos);
            transform.position = world_pos;
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
}
