using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yao : MonoBehaviour
{
    public bool isYang;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 10)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.GetYao(isYang);
            Destroy(gameObject);
        }
    }
}
