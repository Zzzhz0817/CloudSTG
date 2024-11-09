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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLayer(int layer)
    {
        spriteRenderer.sprite = shields[layer];
    }
}
