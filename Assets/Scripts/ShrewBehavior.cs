using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrewBehavior : DamageableEntity
{
    public float speed = 5f;                //Enemy move speed
    public float shootRadius;         //Range at which enemy starts shooting
    public float idealRange;            //Distance Shrew wants to keep player at
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
        if (Vector2.Distance(transform.position, player.transform.position) < shootRadius)
        {
            //Shrew should start shooting towards player
            
        }

        if (Vector2.Distance(transform.position, player.transform.position) < idealRange) { 
            //shrew should start backing away
            //probably can use an inverted form of movetowards
        }

    }
}
