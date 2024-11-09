using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Transform playerTrans;
    private bool isAttracted = false;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 10)
        {
            Destroy(gameObject);
        }


        Vector3 toPlayer = playerTrans.position - transform.position;
        if (toPlayer.magnitude < 2)
        {
            isAttracted = true;
        }

        if (isAttracted)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.GetMedicine();
            Destroy(gameObject);
        }
    }
}
