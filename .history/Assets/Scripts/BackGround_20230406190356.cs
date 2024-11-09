using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] List<GameObject> clouds;
    //[SerializeField] List<GameObject> highClouds;
    private int highCounter = 0;
    private int lowCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highCounter++;
        lowCounter++;
        transform.position = transform.position - Time.deltaTime * (new Vector3(0f, 0.3f, 0f));
        if (Random.Range(0f, 1f) < 0.00005f * lowCounter)
        {
            var cloud = Instantiate(clouds[Random.Range(0, 7)], new Vector3(Random.Range(-2f, 2f),8f,0f), Quaternion.identity);
            Destroy(cloud, 10000f);
            lowCounter = 0;
        }

    }
}
