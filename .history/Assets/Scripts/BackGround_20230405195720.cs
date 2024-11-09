using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] List<GameObject> lowClouds;
    [SerializeField] List<GameObject> highClouds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - Time.deltaTime * (new Vector3(0f, 0.3f, 0f));
        if (Random.Range(0f, 1f) < 0.01f)
        {
            Instantiate(lowClouds[Random.Range(0, 3)], new Vector3(Random.Range(-2f, 2f),8f,0f), Quaternion.identity);
        }
        if (Random.Range(0f, 1f) < 0.01f)
        {
            Instantiate(highClouds[Random.Range(0, 3)], new Vector3(Random.Range(-2f, 2f),8f,0f), Quaternion.identity);
        }
    }
}
