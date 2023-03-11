using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : DamageableEntity
{
    public float speed = 5f;                //Enemy move speed
    public float detectRadius = 1f;         //Enemy detection radius
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < detectRadius)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    //Active only when on screen (Applies to scene view too!)
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
}