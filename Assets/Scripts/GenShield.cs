using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenShield : MonoBehaviour
{
    [SerializeField] List<Sprite> shields;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shields[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLayer(int layer)
    {
        Debug.Log("Gen"+ layer);
        if (layer > 4)
        {
            spriteRenderer.sprite = shields[4];
        }
        else
        {
            spriteRenderer.sprite = shields[layer];
        }
        
    }
}
