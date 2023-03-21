using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : DamageableEntity
{
    public float speed = 5f;                //Enemy move speed

    public float attackRange;            //Distance mouse can attack at
    public GameObject player;
    public GameObject footstep;            //Footstep SFX prefab
    //private EnemyFootstepSFX footstepSFX;
    private Transform parent;


    void Start()
    {
        parent = GetComponent<Transform>();
        //footstepSFX = new EnemyFootstepSFX(footstep, parent);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            //mouse starts attacking
        }
        else { 
            //Move towards player
        }

    }
}
