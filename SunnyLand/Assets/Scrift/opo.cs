using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opo : MonoBehaviour
{
    public GameObject player;
    private Transform playerpos;
    private Vector2 currentpos;
    public float distance;
    public int speedEnemy;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerpos = player.GetComponent<Transform>();
        currentpos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,playerpos.position)<distance)
        {
            rb.velocity = new Vector2(-speedEnemy, rb.velocity.y);
        }
        else
        {
            if(Vector2.Distance(transform.position,currentpos)<= 0)
            {

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentpos, speedEnemy * Time.deltaTime);
            }

        }
    }
    
}

