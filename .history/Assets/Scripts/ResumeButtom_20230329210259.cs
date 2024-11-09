using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtom : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        if (Input.touchCount == 0)
        {
            transform.position = player.transform.position;
        } 
    }
    
}
